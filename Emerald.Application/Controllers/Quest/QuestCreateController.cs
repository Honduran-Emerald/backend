using Emerald.Application.Models.Quest.Module;
using Emerald.Application.Models.Response.Quest;
using Emerald.Application.Services.Factories;
using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Domain.Repositories;
using Emerald.Domain.Services;
using Emerald.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Application.Controllers.Quest
{
    [ApiController]
    [Route("create")]
    public class QuestCreateController : ControllerBase
    {
        private IQuestRepository questRepository;
        private IComponentRepository componentRepository;
        private ITrackerRepository trackerRepository;
        private IModuleRepository moduleRepository;

        private QuestCreateService questCreateService;
        private QuestModelFactory questModelFactory;
        private ModuleModelFactory moduleModelFactory;

        public QuestCreateController(IQuestRepository questRepository, IComponentRepository componentRepository, ITrackerRepository trackerRepository, IModuleRepository moduleRepository, QuestCreateService questCreateService, QuestModelFactory questModelFactory, ModuleModelFactory moduleModelFactory)
        {
            this.questRepository = questRepository;
            this.componentRepository = componentRepository;
            this.trackerRepository = trackerRepository;
            this.moduleRepository = moduleRepository;
            this.questCreateService = questCreateService;
            this.questModelFactory = questModelFactory;
            this.moduleModelFactory = moduleModelFactory;
        }

        /// <summary>
        /// Queries all information about a single by the user created quest
        /// </summary>
        /// <param name="questId"></param>
        /// <returns></returns>
        [HttpGet("query")]
        public async Task<ActionResult<QuestCreateQueryResponse>> Query(
            [FromQuery] ObjectId questId)
        {
            try
            {
                Domain.Models.QuestAggregate.Quest quest = await questRepository.Get(questId);
                QuestVersion questVersion = quest.GetDevelopmentQuestVersion();

                return Ok(new QuestCreateQueryResponse
                {
                    Modules = await moduleModelFactory.Create(await moduleRepository.GetForQuest(questVersion)),
                    Quest = await questModelFactory.Create(quest),
                    FirstModuleId = questVersion.FirstModule.ToString()
                });
            }
            catch (DomainException exception)
            {
                return BadRequest(new 
                {
                    Message = exception.Message
                });
            }
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
            QuestVersion? stableQuestVersion = quest.GetStableQuestVersion();

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
