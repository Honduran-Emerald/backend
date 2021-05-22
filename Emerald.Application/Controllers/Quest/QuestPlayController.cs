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

namespace Emerald.Application.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/play")]
    public class QuestPlayController : ControllerBase
    {
        private ITrackerRepository trackerRepository;
        private IUserRepository userRepository;

        private ResponseEventModelFactory responseEventFactory;
        private TrackerModelFactory trackerFactory;
        private QuestPlayService questPlayService;

        public QuestPlayController(ITrackerRepository trackerRepository, IUserRepository userRepository, ResponseEventModelFactory responseEventFactory, TrackerModelFactory trackerFactory, QuestPlayService questPlayService)
        {
            this.trackerRepository = trackerRepository;
            this.userRepository = userRepository;
            this.responseEventFactory = responseEventFactory;
            this.trackerFactory = trackerFactory;
            this.questPlayService = questPlayService;
        }

        [HttpGet("query")]
        public async Task<ActionResult<QuestPlayQueryResponse>> Query(
            [FromForm] QuestPlayQueryRequest request)
        {
            User user = await  userRepository.Get(User);

            IMongoQueryable<Tracker> queryable = trackerRepository.GetQueryable()
                .Where(t => t.UserId == user.Id);

            if (request.Unfinished)
            {
                queryable = queryable.Where(t => t.Finished == false);
            }

            return Ok(new QuestPlayQueryResponse(
                trackers: await trackerFactory.Create(await queryable.ToListAsync())
            ));
        }

        [HttpGet("querytrackernodes")]
        public async Task<IActionResult> QueryTrackerNodes()
        {
            return Ok();
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
            try
            {
                ResponseEventCollection responseEvent = await questPlayService.HandleEvent(
                    await userRepository.Get(User),
                    new ChoiceRequestEvent(choiceEvent.Choice));

                return Ok(new QuestPlayEventResponse
                {
                    ResponseEventCollection = await responseEventFactory.Create(responseEvent)
                });
            }
            catch (DomainException e)
            {
                return BadRequest(new
                {
                    message = e.Message
                });
            }
        }
    }
}
