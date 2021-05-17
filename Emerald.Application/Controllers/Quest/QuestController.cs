using Emerald.Application.Models.Quest;
using Emerald.Application.Models.Response.Quest;
using Emerald.Application.Services.Factories;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Infrastructure;
using Emerald.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emerald.Application.Controllers.Quest
{
    [ApiController]
    [Route("/quest")]
    public class QuestController : ControllerBase 
    {
        private IConfiguration configuration;
        private IQuestRepository questRepository;
        private IQuestModelFactory questModelFactory;

        public QuestController(IConfiguration configuration, IQuestRepository questRepository, IQuestModelFactory questModelFactory)
        {
            this.configuration = configuration;
            this.questRepository = questRepository;
            this.questModelFactory = questModelFactory;
        }

        [HttpGet("query")]
        public async Task<ActionResult<QuestQueryResponse>> Query(
            [FromBody] int offset)
        {
            List<QuestModel> questModels = new List<QuestModel>();

            foreach (var quest in questRepository.GetQueryable()
                    .Skip(offset)
                    .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize"))
                    .ToEnumerable())
            {
                questModels.Add(await questModelFactory.Create(quest));
            }

            return Ok(new QuestQueryResponse
            {
                Quests = questModels
            });
        }
    }
}
