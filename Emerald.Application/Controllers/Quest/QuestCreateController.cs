using Emerald.Application.Models.Quest.Module;
using Emerald.Application.Models.Request.Quest;
using Emerald.Application.Models.Response.Quest;
using Emerald.Application.Services.Factories;
using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.PrototypeAggregate;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Domain.Repositories;
using Emerald.Domain.Services;
using Emerald.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Application.Controllers.Quest
{
    [ApiController]
    [Route("create")]
    public class QuestCreateController : ControllerBase
    {
        private IComponentRepository componentRepository;
        private IQuestRepository questRepository;
        private IQuestPrototypeRepository questPrototypeRepository;
        private IModuleRepository moduleRepository;
        private ITrackerRepository trackerRepository;

        private QuestCreateService questCreateService;
        private QuestModelFactory questModelFactory;
        private ModuleModelFactory moduleModelFactory;
        public QuestCreateController(IComponentRepository componentRepository, IQuestRepository questRepository, IQuestPrototypeRepository questPrototypeRepository, IModuleRepository moduleRepository, ITrackerRepository trackerRepository, QuestCreateService questCreateService, QuestModelFactory questModelFactory, ModuleModelFactory moduleModelFactory)
        {
            this.componentRepository = componentRepository;
            this.questRepository = questRepository;
            this.questPrototypeRepository = questPrototypeRepository;
            this.moduleRepository = moduleRepository;
            this.trackerRepository = trackerRepository;
            this.questCreateService = questCreateService;
            this.questModelFactory = questModelFactory;
            this.moduleModelFactory = moduleModelFactory;
        }

        /// <summary>
        /// Queries meta information about all created quests by this user
        /// </summary>
        /// <returns></returns>
        [HttpGet("query")]
        public async Task<ActionResult> Query(
            [FromQuery] int offset)
        {
            return Ok(new
            {
                Quests = await questRepository.GetQueryable().Select(q => q.Id).ToListAsync()
            });
        }

        /// <summary>
        /// Queries all information about a single by the user created quest
        /// </summary>
        /// <param name="questId"></param>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<ActionResult<QuestCreateGetResponse>> Get(
            [FromQuery] ObjectId questId)
        {
            Domain.Models.QuestAggregate.Quest quest = await questRepository.Get(questId);

            return Ok(new QuestCreateGetResponse(
                await questPrototypeRepository.Get(quest.PrototypeId)));
        }

        /// <summary>
        /// Update a prototype of a quest. The Quest has to be queried before with create/query
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("put")]
        public async Task<ActionResult<QuestCreateGetResponse>> Put(
            [FromBody] QuestCreatePutRequest request)
        {
            Domain.Models.QuestAggregate.Quest quest = await questRepository.Get(request.QuestId);

            if (quest.PrototypeId != request.QuestPrototype.Id)
            {
                throw new DomainException("Got invalid prototype id");
            }

            await questPrototypeRepository.Update(request.QuestPrototype);

            return Ok();
        }

        /// <summary>
        /// Publish the development version of a quest to make it stable
        /// </summary>
        /// <param name="questId"></param>
        /// <returns></returns>
        [HttpPost("publish")]
        public async Task<IActionResult> Publish(
            [FromBody] ObjectId questId)
        {
            Domain.Models.QuestAggregate.Quest quest = await questRepository.Get(questId);
            QuestVersion? stableQuestVersion = quest.GetCurrentPrivateQuestVersion();

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

            await questCreateService.Publish(quest);
            return Ok();
        }
    }
}
