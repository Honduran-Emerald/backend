using Emerald.Application.Models.Quest;
using Emerald.Application.Models.Response.Quest;
using Emerald.Application.Services;
using Emerald.Application.Services.Factories;
using Emerald.Domain.Models.TrackerAggregate;
using Emerald.Domain.Models.UserAggregate;
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
                    .Where(q => q.Public || q.OwnerUserId == user.Id)
                    .Where(q => q.QuestVersions.Count > 0)
                    .Where(q => q.Locks.Count == 0);

            if (request.OwnerId != null)
            {
                queryable = queryable.Where(q => q.OwnerUserId == request.OwnerId);
            }

            var quests = await queryable
                    .Skip(request.Offset)
                    .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize"))
                    .ToListAsync();

            return Ok(new QuestQueryResponse(
                quests: await questModelFactory.Create(
                    quests.Select(q => new QuestPairModel(q, q.QuestVersions
                                        .OrderByDescending(v => v.Version)
                                        .FirstOrDefault()))
                        .ToList())));
        }

        /// <summary>
        /// Query all quests with the specified vote by the authorized user
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="voteType"></param>
        /// <returns></returns>
        [HttpGet("queryvoted")]
        public async Task<ActionResult<QuestQueryResponse>> QueryVoted(
            [FromQuery] int offset,
            [FromQuery] VoteType voteType,
            [FromServices] ITrackerRepository trackerRepository,
            [FromServices] IQuestRepository questRepository,
            [FromServices] QuestModelFactory questModelFactory,
            [FromServices] IUserService userService)
        {
            User user = await userService.CurrentUser();
            List<QuestPairModel> quests = new List<QuestPairModel>();

            foreach (Tracker tracker in await trackerRepository.GetQueryable()
                .Where(t => t.UserId == user.Id)
                .Where(t => t.Vote == voteType)
                .Skip(offset)
                .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize"))
                .ToListAsync())
            {
                var quest = await questRepository.Get(tracker.QuestId);

                quests.Add(new QuestPairModel(
                    quest, quest.GetQuestVersion(tracker.QuestVersion)));
            }

            return Ok(new QuestQueryResponse(
                await questModelFactory.Create(quests)));
        }

        /// <summary>
        /// Query all quests with the specified finish state by the authorized user
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="finished"></param>
        /// <returns></returns>
        [HttpGet("queryfinished")]
        public async Task<ActionResult<QuestQueryResponse>> QueryFinished(
            [FromQuery] int offset,
            [FromQuery] bool finished,
            [FromServices] ITrackerRepository trackerRepository,
            [FromServices] IQuestRepository questRepository,
            [FromServices] QuestModelFactory questModelFactory,
            [FromServices] IUserService userService)
        {
            User user = await userService.CurrentUser();
            List<QuestPairModel> quests = new List<QuestPairModel>();

            foreach (Tracker tracker in await trackerRepository.GetQueryable()
                .Where(t => t.UserId == user.Id)
                .Where(t => t.Finished == finished)
                .Skip(offset)
                .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize"))
                .ToListAsync())
            {
                var quest = await questRepository.Get(tracker.QuestId);

                quests.Add(new QuestPairModel(
                    quest, quest.GetQuestVersion(tracker.QuestVersion)));
            }

            return Ok(new QuestQueryResponse(
                await questModelFactory.Create(quests)));
        }
    }
}
