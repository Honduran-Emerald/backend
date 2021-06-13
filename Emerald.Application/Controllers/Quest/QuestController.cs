﻿using Emerald.Application.Models.Quest;
using Emerald.Application.Models.Response.Quest;
using Emerald.Application.Services.Factories;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Controllers.Quest
{
    [ApiController]
    [Route("/quest")]
    public class QuestController : ControllerBase
    {
        private IUserRepository userRepository;
        private IConfiguration configuration;
        private IQuestRepository questRepository;
        private QuestModelFactory questModelFactory;

        public QuestController(IUserRepository userRepository, IConfiguration configuration, IQuestRepository questRepository, QuestModelFactory questModelFactory)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
            this.questRepository = questRepository;
            this.questModelFactory = questModelFactory;
        }

        /// <summary>
        /// Query quests based on specified settings without tracker information
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("query")]
        public async Task<ActionResult<QuestQueryResponse>> Query(
            [FromQuery] int offset)
        {
            return Ok(new QuestQueryResponse(
                quests: await questModelFactory.Create(
                    quests.Select(q => new QuestPairModel(q, q.QuestVersions
                                        .OrderByDescending(v => v.Version)
                                        .FirstOrDefault()))
                        .ToList())));
        }
    }
}
