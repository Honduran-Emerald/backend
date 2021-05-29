using Emerald.Application.Models.Response;
using Emerald.Application.Models.Quest.Events;
using Emerald.Application.Services;
using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.ModuleAggregate.RequestEvents;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;
using Emerald.Application.Models.Response.Quest;
using Emerald.Application.Models.Request;
using Emerald.Infrastructure.Repositories;
using MongoDB.Driver.Linq;
using Emerald.Domain.Models.TrackerAggregate;
using Microsoft.EntityFrameworkCore;
using Emerald.Application.Services.Factories;
using Emerald.Application.Models.Request.Quest;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Domain.Repositories;
using MongoDB.Bson;
using System.Linq;

namespace Emerald.Application.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/play")]
    public class QuestPlayController : ControllerBase
    {
        private IQuestRepository questRepository;
        private IComponentRepository componentRepository;
        private IModuleRepository moduleRepository;
        private ITrackerRepository trackerRepository;
        private IUserRepository userRepository;

        private ModuleModelFactory moduleFactory;
        private QuestPlayService questPlayService;
        private ResponseEventModelFactory responseEventFactory;
        private TrackerModelFactory trackerFactory;
        private TrackerNodeModelFactory trackerNodeFactory;

        public QuestPlayController(IQuestRepository questRepository, IComponentRepository componentRepository, IModuleRepository moduleRepository, ITrackerRepository trackerRepository, IUserRepository userRepository, ModuleModelFactory moduleFactory, QuestPlayService questPlayService, ResponseEventModelFactory responseEventFactory, TrackerModelFactory trackerFactory, TrackerNodeModelFactory trackerNodeFactory)
        {
            this.questRepository = questRepository;
            this.componentRepository = componentRepository;
            this.moduleRepository = moduleRepository;
            this.trackerRepository = trackerRepository;
            this.userRepository = userRepository;
            this.moduleFactory = moduleFactory;
            this.questPlayService = questPlayService;
            this.responseEventFactory = responseEventFactory;
            this.trackerFactory = trackerFactory;
            this.trackerNodeFactory = trackerNodeFactory;
        }



        /// <summary>
        /// Query based on specified settings already tracked quests
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("query")]
        public async Task<ActionResult<QuestPlayQueryResponse>> Query(
            [FromQuery] QuestPlayQueryRequest request)
        {
            User user = await  userRepository.Get(User);

            IMongoQueryable<Tracker> queryable = trackerRepository.GetQueryable()
                .Where(t => t.UserId == user.Id);

            if (request.Unfinished)
            {
                queryable = queryable.Where(t => t.Finished == false);
            }

            return Ok(new QuestPlayQueryResponse(
                trackers: await trackerFactory.Create(queryable.ToList())
            ));
        }

        /// <summary>
        /// Create a quest tracker for a single quest. Use to start playing a quest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult<QuestPlayCreateRequest>> Create(
            [FromBody] QuestPlayCreateRequest request)
        {
            User user = await userRepository.Get(User);
            Domain.Models.QuestAggregate.Quest quest = await questRepository.Get(request.QuestId);

            QuestVersion? questVersion = quest.GetCurrentQuestVersion();

            if (questVersion == null)
            {
                return BadRequest(new
                {
                    Message = "Tracker can not be created for unpublished quest"
                });
            }

            if (await trackerRepository.GetQueryable()
                    .Where(t => t.UserId == user.Id && t.QuestId == request.QuestId)
                    .AnyAsync())
            {
                return BadRequest(new
                {
                    Message = "Tracker can not be created for already tracked quest"
                });
            }
            
            Tracker tracker = new Tracker(
                user.Id,
                quest,
                questVersion);

            Module module = await moduleRepository.Get(questVersion.FirstModuleId);
            tracker.AddTrackerPath(new TrackerNode(module.Id));

            user.AddTracker(tracker);

            await trackerRepository.Add(tracker);
            await userRepository.Update(user);

            return Ok(new QuestPlayCreateResponse(
                await trackerFactory.Create(tracker),
                await trackerNodeFactory.Create(tracker.Path.First())));
        }

        /// <summary>
        /// Query trackernodes with all module and memento information for a single playing quest
        /// </summary>
        /// <returns></returns>
        [HttpGet("querytrackernodes")]
        public async Task<ActionResult<QuestPlayQueryTrackerNodesResponse>> QueryTrackerNodes(
            [FromQuery] ObjectId trackerId)
        {
            Tracker tracker = await trackerRepository.Get(trackerId);

            return Ok(new QuestPlayQueryTrackerNodesResponse(
                await trackerNodeFactory.Create(tracker.Path)));
        }

        [HttpPost("event/position")]
        public async Task<IActionResult> HandleEvent(PositionRequestEventModel positionRequest)
        {


            return Ok();
        }

        [HttpPost("event/choice")]
        public async Task<ActionResult<QuestPlayEventResponse>> HandleEvent(
            [FromBody] ChoiceRequestEventModel choiceEvent)
        {
            ResponseEventCollection responseEvent = await questPlayService.HandleEvent(
                await userRepository.Get(User),
                choiceEvent.TrackerId,
                new ChoiceRequestEvent(choiceEvent.Choice));

            return Ok(new QuestPlayEventResponse
            {
                ResponseEventCollection = await responseEventFactory.Create(responseEvent)
            });
        }
    }
}
