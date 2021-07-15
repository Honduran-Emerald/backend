using Emerald.Application.Models;
using Emerald.Application.Models.Prototype;
using Emerald.Application.Models.Request.Quest;
using Emerald.Application.Models.Response.Quest;
using Emerald.Application.Services;
using Emerald.Application.Services.Factories;
using Emerald.Domain.Models;
using Emerald.Domain.Models.ComponentAggregate;
using Emerald.Domain.Models.LockAggregate;
using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.PrototypeAggregate;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestPrototypeAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Domain.Models.TrackerAggregate;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Domain.Repositories;
using Emerald.Domain.Services;
using Emerald.Infrastructure.Repositories;
using Emerald.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Application.Controllers.Quest
{
    [Authorize]
    [ApiController]
    [Route("create")]
    public class QuestCreateController : ControllerBase
    {
        /// <summary>
        /// Query all quests created by the authorized user with special creator information
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet("query")]
        public async Task<ActionResult<QuestCreateQueryResponse>> Query(
            [FromQuery] int offset,
            [FromServices] IConfiguration configuration,
            [FromServices] IUserService userService,
            [FromServices] IQuestRepository questRepository,
            [FromServices] QuestPrototypeModelFactory questPrototypeModelFactory)
        {
            User user = await userService.CurrentUser();

            var quests = await questRepository.GetQueryable()
                .Where(q => q.OwnerUserId == user.Id)
                .Skip(offset)
                .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize"))
                .ToListAsync();

            return Ok(new QuestCreateQueryResponse(await questPrototypeModelFactory.Create(quests)));
        }

        /// <summary>
        /// Create new quest together with a new questprototype
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult<QuestCreateCreateResponse>> Create(
            [FromBody] QuestCreateCreateRequest request,
            [FromServices] IUserRepository userRepository,
            [FromServices] IUserService userService,
            [FromServices] IQuestRepository questRepository,
            [FromServices] IQuestPrototypeRepository questPrototypeRepository)
        {
            User user = await userService.CurrentUser();
            QuestPrototype questPrototype = new QuestPrototype();

            await questPrototypeRepository.Add(questPrototype);

            var quest = new Domain.Models.QuestAggregate.Quest(
                user, questPrototype);

            user.QuestIds.Add(quest.Id);
            await userRepository.Update(user);
            await questRepository.Add(quest);

            return Ok(new QuestCreateCreateResponse(quest.Id, questPrototype));
        }

        /// <summary>
        /// Delete a quest if owned by authorized user
        /// </summary>
        /// <param name="questId"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(
            [FromBody] ObjectId questId,
            [FromServices] IUserRepository userRepository,
            [FromServices] IComponentRepository componentRepository,
            [FromServices] IModuleRepository moduleRepository,
            [FromServices] IQuestRepository questRepository,
            [FromServices] ITrackerRepository trackerRepository,
            [FromServices] IUserService userService)
        {
            User user = await userService.CurrentUser();
            Domain.Models.QuestAggregate.Quest quest = await questRepository.Get(questId);

            user.QuestIds.Remove(quest.Id);

            if (quest.OwnerUserId != user.Id)
            {
                return BadRequest(new
                {
                    Message = "User is not the owner of the quest"
                });
            }

            foreach (QuestVersion questVersion in quest.QuestVersions)
                foreach (ObjectId moduleId in questVersion.ModuleIds)
                {
                    Module module = await moduleRepository.Get(moduleId);

                    foreach (ObjectId componentId in module.ComponentIds)
                    {
                        try
                        {
                            await componentRepository.Remove(
                                await componentRepository.Get(componentId));
                        }
                        catch(Exception e)
                        {
                        }
                    }

                    try
                    {
                        await moduleRepository.Remove(module);
                    }
                    catch (Exception e)
                    {
                    }
                }

            foreach (Tracker tracker in await trackerRepository.GetQueryable()
                .Where(t => t.QuestId == quest.Id)
                .ToListAsync())
            {
                await trackerRepository.Remove(tracker);
            }

            user.QuestIds.Remove(quest.Id);
            await userRepository.Update(user);
            await questRepository.Remove(quest);

            return Ok();
        }

        /// <summary>
        /// Query all information about a single by the authorized user created quest
        /// </summary>
        /// <param name="questId"></param>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<ActionResult<QuestCreateGetResponse>> Get(
            [FromQuery] ObjectId questId,
            [FromServices] IQuestRepository questRepository,
            [FromServices] IQuestPrototypeRepository questPrototypeRepository)
        {
            Domain.Models.QuestAggregate.Quest quest = await questRepository.Get(questId);

            QuestPrototype prototype = await questPrototypeRepository.Get(quest.PrototypeId);
            return Ok(new QuestCreateGetResponse(prototype));
        }

        /// <summary>
        /// Update a prototype of a quest. The Quest has to be queried before with create/query
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("put")]
        public async Task<ActionResult<QuestCreateGetResponse>> Put(
            [FromBody] QuestCreatePutRequest request,
            [FromServices] IQuestRepository questRepository,
            [FromServices] IQuestPrototypeRepository questPrototypeRepository,
            [FromServices] IImageIndexService imageIndexService,
            [FromServices] IImageService imageService)
        {
            Domain.Models.QuestAggregate.Quest quest = await questRepository.Get(request.QuestId);

            if (quest.PrototypeId != request.QuestPrototype.Id)
            {
                throw new DomainException("Got invalid prototype id");
            }

            QuestPrototype questPrototype = await questPrototypeRepository.Get(quest.PrototypeId);

            // ensure user has not added new images by himself
            if (request.QuestPrototype.Images.Any(i1 => questPrototype.Images.Any(i2 => i2.ImageId == i1.ImageId) == false))
            {
                return BadRequest(new
                {
                    Message = "QuestPrototype contains invalid images"
                });
            }

            List<ImagePrototype> newImages = new List<ImagePrototype>();

            foreach (ImageModel image in request.NewImages)
            {
                byte[] binary = Convert.FromBase64String(image.Image);
                string imageId = await imageService.Upload(new MemoryStream(binary));

                ImagePrototype prototype = new ImagePrototype(image.Reference, imageId);
                newImages.Add(prototype);
                request.QuestPrototype.Images.Add(prototype);
            }

            await questPrototypeRepository.Update(request.QuestPrototype);

            quest.Outdated = true;
            await questRepository.Update(quest);

            return Ok(new QuestCreatePutResponse(newImages));
        }

        /// <summary>
        /// Publish the development version of a quest to make it stable
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("release")]
        public async Task<IActionResult> Publish(
            [FromBody] QuestCreatePublishRequest request,
            [FromServices] IModuleRepository moduleRepository,
            [FromServices] ITrackerRepository trackerRepository,
            [FromServices] IComponentRepository componentRepository,
            [FromServices] IQuestRepository questRepository,
            [FromServices] IQuestPrototypeRepository questPrototypeRepository,
            [FromServices] QuestCreateService questCreateService)
        {
            Domain.Models.QuestAggregate.Quest quest = await questRepository.Get(request.QuestId);
            QuestVersion? stableQuestVersion = quest.GetCurrentQuestVersion();

            if (quest.IsLocked())
            {
                return StatusCode(252);
            }

            QuestPrototype questPrototype = await questPrototypeRepository.Get(quest.PrototypeId);

            await questCreateService.Publish(
                questPrototype, 
                quest, 
                new QuestPrototypeContext(
                    questPrototype.Modules.ToDictionary(m => m.Id, _ => ObjectId.GenerateNewId()),
                    questPrototype.Images.ToDictionary(i => i.Reference, i => i.ImageId)));

            if (stableQuestVersion != null &&
                await trackerRepository.HasAnyTrackerForQuest(quest) == false)
            {
                foreach (Module module in await moduleRepository.GetForQuest(stableQuestVersion))
                {
                    await componentRepository.RemoveAll(module.ComponentIds);
                    await moduleRepository.Remove(module);
                }

                quest.RemoveQuestVersion(stableQuestVersion);
            }

            return Ok();
        }
    }

    internal class QuestPrototypeContext : IPrototypeContext
    {
        private Dictionary<int, ObjectId> modules;
        private Dictionary<int, string> images;

        public QuestPrototypeContext(Dictionary<int, ObjectId> modules, Dictionary<int, string> images)
        {
            this.modules = modules;
            this.images = images;
        }

        public string ConvertImageId(int reference)
        {
            return images[reference];
        }

        public ObjectId ConvertModuleId(int moduleId)
        {
            return modules[moduleId];
        }
    }
}
