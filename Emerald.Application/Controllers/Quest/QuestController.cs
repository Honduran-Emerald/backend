using Emerald.Application.Models.Quest;
using Emerald.Application.Models.Response.Quest;
using Emerald.Application.Services.Factories;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Infrastructure;
using Emerald.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
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
        private QuestModelFactory questModelFactory;

        public QuestController(IConfiguration configuration, IQuestRepository questRepository, QuestModelFactory questModelFactory)
        {
            this.configuration = configuration;
            this.questRepository = questRepository;
            this.questModelFactory = questModelFactory;
        }

        /// <summary>
        /// Query quests based on specified settings without tracker information
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("query")]
        public async Task<ActionResult<QuestQueryResponse>> Query(
            [FromQuery] int offset)
        {
            return Ok(new QuestQueryResponse(
                quests: await questModelFactory.Create(await questRepository.GetQueryable()
                    .Skip(offset)
                    .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize"))
                    .ToListAsync())));
        }
    }
}
