using Emerald.Application.Models.Quest;
using Emerald.Application.Models.Response.Quest;
using Emerald.Application.Services.Factories;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Infrastructure;
using Emerald.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
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
            [FromQuery] QuestQueryRequest request)
        {
            User user = await userRepository.Get(User);

            var queryable = questRepository.GetQueryable()
                    .Skip(request.Offset)
                    .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize"))
                    .Where(q => q.Public || q.OwnerUserId == user.Id);

            if (request.OwnerId != null)
            {
                queryable = queryable.Where(q => q.OwnerUserId == request.OwnerId);
            }

            var quests = await queryable.ToListAsync();

            return Ok(new QuestQueryResponse(
                quests: await questModelFactory.Create(
                    quests.Select(q => new QuestPairModel(q, q.QuestVersions
                                        .OrderByDescending(v => v.Version)
                                        .FirstOrDefault()))
                        .ToList())));
        }
    }
}
