using Emerald.Application.Models.Quest.Module;
using Emerald.Application.Models.Response.Quest;
using Emerald.Application.Services.Factories;
using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Repositories;
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
        private IModuleRepository moduleRepository;
        private QuestModelFactory questModelFactory;
        private ModuleModelFactory moduleModelFactory;

        public QuestCreateController(IQuestRepository questRepository, IModuleRepository moduleRepository, QuestModelFactory questModelFactory, ModuleModelFactory moduleModelFactory)
        {
            this.questRepository = questRepository;
            this.moduleRepository = moduleRepository;
            this.questModelFactory = questModelFactory;
            this.moduleModelFactory = moduleModelFactory;
        }

        [HttpGet("query")]
        public async Task<ActionResult<QuestCreateQueryResponse>> Query(
            [FromQuery] ObjectId questId)
        {
            try
            {
                Domain.Models.QuestAggregate.Quest quest = await questRepository.Get(questId);

                return Ok(new QuestCreateQueryResponse
                {
                    Modules = await moduleModelFactory.Create(await moduleRepository.GetForQuest(quest.GetStableQuestVersion())),
                    Quest = await questModelFactory.Create(quest)
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
    }
}
