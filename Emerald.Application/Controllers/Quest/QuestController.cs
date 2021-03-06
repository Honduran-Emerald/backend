using Emerald.Application.Models;
using Emerald.Application.Models.Quest;
using Emerald.Application.Models.Response.Quest;
using Emerald.Application.Services;
using Emerald.Application.Services.Factories;
using Emerald.Domain.Models.TrackerAggregate;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Infrastructure.Repositories;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Controllers.Quest
{
    [Authorize]
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
        [HttpGet("query")]
        public async Task<ActionResult<QuestQueryResponse>> Query(
            [FromQuery] QuestQueryRequest request)
        {
            User user = await userRepository.Get(User);

            var mongoQueryable = questRepository.GetQueryable()
                .Where(q => (q.Public || q.OwnerUserId == user.Id)
                            && q.QuestVersions.Count > 0
                            && q.Locks.Count == 0);

            if (request.OwnerId != null)
            {
                mongoQueryable = mongoQueryable.Where(q => q.OwnerUserId == request.OwnerId);
            }

            var queryable = (IEnumerable<Domain.Models.QuestAggregate.Quest>) await mongoQueryable
                .ToListAsync();

            if (request.Location != null)
            {
                queryable = queryable.OrderBy(q => new GeoCoordinate(q.GetCurrentQuestVersion()!.Location.Coordinates.Latitude,
                                                q.GetCurrentQuestVersion()!.Location.Coordinates.Longitude).GetDistanceTo(
                    new GeoCoordinate(request.Location.Latitude, request.Location.Longitude)))
                    .Where(q => new GeoCoordinate(q.GetCurrentQuestVersion()!.Location.Coordinates.Latitude,
                                                q.GetCurrentQuestVersion()!.Location.Coordinates.Longitude).GetDistanceTo(
                    new GeoCoordinate(request.Location.Latitude, request.Location.Longitude)) < request.Radius);
            }

            return Ok(new QuestQueryResponse(
                quests: await questModelFactory.Create(
                    queryable.Skip(request.Offset)
                        .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize"))
                        .ToList()
                        .Select(q => new QuestPairModel(q, q.QuestVersions
                                        .OrderByDescending(v => v.Version)
                                        .FirstOrDefault()))
                        .ToList())));
        }

        /// <summary>
        /// Query quests based on name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("queryname")]
        public async Task<ActionResult<QuestQueryResponse>> QueryName(
            [FromQuery, Required] int offset,
            [FromQuery] LocationModel? location,
            [FromQuery] float? radius,
            [FromQuery, Required] string search)
        {
            User user = await userRepository.Get(User);

            var mongoQueryable = questRepository.GetQueryable()
                .Where(q => (q.Public || q.OwnerUserId == user.Id)
                            && q.QuestVersions.Count > 0
                            && q.Locks.Count == 0);

            var queryable = (IEnumerable<Domain.Models.QuestAggregate.Quest>)await mongoQueryable
                .ToListAsync();

            if (location != null)
            {
                queryable = queryable.OrderBy(q => new GeoCoordinate(q.GetCurrentQuestVersion()!.Location.Coordinates.Latitude,
                                                q.GetCurrentQuestVersion()!.Location.Coordinates.Longitude).GetDistanceTo(
                    new GeoCoordinate(location.Latitude, location.Longitude)))
                    .Where(q => new GeoCoordinate(q.GetCurrentQuestVersion()!.Location.Coordinates.Latitude,
                                                q.GetCurrentQuestVersion()!.Location.Coordinates.Longitude).GetDistanceTo(
                    new GeoCoordinate(location.Latitude, location.Longitude)) < radius);
            }

            queryable = queryable.Where(q => q.GetCurrentQuestVersion()!.Title.ToUpper().Contains(search.ToUpper()));

            return Ok(new QuestQueryResponse(
                quests: await questModelFactory.Create(
                    queryable.Skip(offset)
                        .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize"))
                        .ToList()
                        .Select(q => new QuestPairModel(q, q.QuestVersions
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
            [FromQuery] ObjectId? userId,
            [FromQuery] int offset,
            [FromQuery] VoteType voteType,
            [FromServices] ITrackerRepository trackerRepository,
            [FromServices] IQuestRepository questRepository,
            [FromServices] QuestModelFactory questModelFactory,
            [FromServices] IUserService userService)
        {
            User user;

            if (userId.HasValue)
            {
                user = await userRepository.Get(userId.Value);
            }
            else
            {
                user = await userService.CurrentUser();
            }

            List<QuestPairModel> quests = new List<QuestPairModel>();

            foreach (Tracker tracker in await trackerRepository.GetQueryable()
                .Where(t => t.UserId == user.Id)
                .Where(t => t.Vote == voteType)
                .Skip(offset)
                .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize"))
                .ToListAsync())
            {
                var quest = await questRepository.Get(tracker.QuestId);
                
                if (quest.Public && quest.QuestVersions.Count > 0
                                 && quest.Locks.Count == 0)
                {
                    quests.Add(new QuestPairModel(
                        quest, quest.GetQuestVersion(tracker.QuestVersion)));
                }
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
            [FromQuery] ObjectId? userId,
            [FromQuery] int offset,
            [FromQuery] bool finished,
            [FromServices] ITrackerRepository trackerRepository,
            [FromServices] IQuestRepository questRepository,
            [FromServices] QuestModelFactory questModelFactory,
            [FromServices] IUserService userService)
        {
            User user;

            if (userId.HasValue)
            {
                user = await userRepository.Get(userId.Value);
            }
            else
            {
                user = await userService.CurrentUser();
            }

            List<QuestPairModel> quests = new List<QuestPairModel>();

            foreach (Tracker tracker in await trackerRepository.GetQueryable()
                .Where(t => t.UserId == user.Id)
                .Where(t => t.Finished == finished)
                .Skip(offset)
                .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize"))
                .ToListAsync())
            {
                var quest = await questRepository.Get(tracker.QuestId);

                if (quest.Public && quest.QuestVersions.Count > 0
                                 && quest.Locks.Count == 0)
                {
                    quests.Add(new QuestPairModel(
                        quest, quest.GetQuestVersion(tracker.QuestVersion)));
                }
            }

            return Ok(new QuestQueryResponse(
                await questModelFactory.Create(quests)));
        }

        /// <summary>
        /// Query quests ordered by creationtime
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="location"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        [HttpGet("querynew")]
        public async Task<ActionResult<QuestQueryResponse>> QueryNew(
            [FromQuery] int offset,
            [FromQuery] LocationModel location,
            [FromQuery] float radius,
            [FromServices] ITrackerRepository trackerRepository,
            [FromServices] IQuestRepository questRepository,
            [FromServices] QuestModelFactory questModelFactory,
            [FromServices] IUserService userService)
        {
            User user = await userRepository.Get(User);

            var mongoQueryable = questRepository.GetQueryable()
                .Where(q => (q.Public || q.OwnerUserId == user.Id)
                            && q.QuestVersions.Count > 0
                            && q.Locks.Count == 0);

            var queryable = (IEnumerable<Domain.Models.QuestAggregate.Quest>) await mongoQueryable.ToListAsync();

            queryable = queryable
                .Where(q => new GeoCoordinate(q.GetCurrentQuestVersion()!.Location.Coordinates.Latitude,
                                            q.GetCurrentQuestVersion()!.Location.Coordinates.Longitude).GetDistanceTo(
                new GeoCoordinate(location.Latitude, location.Longitude)) < radius);
        
            return Ok(new QuestQueryResponse(
                quests: await questModelFactory.Create(
                    queryable
                        .OrderByDescending(q => q.CreationTime)
                        .Skip(offset)
                        .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize"))
                        .ToList()
                        .Select(q => new QuestPairModel(q, q.QuestVersions
                                        .OrderByDescending(v => v.Version)
                                        .FirstOrDefault()))
                        .ToList())));
            /*
            var filter = Builders<Domain.Models.QuestAggregate.Quest>.Filter.And(
                Builders<Domain.Models.QuestAggregate.Quest>.Filter.Near(q => 
                    q.QuestVersions.Last().Location, new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
                        new GeoJson2DGeographicCoordinates(location.Longitude, location.Latitude)), radius),
                Builders<Domain.Models.QuestAggregate.Quest>.Filter.Eq(q =>
                    q.Public && q.QuestVersions.Count > 0
                             && q.Locks.Count == 0, true));
            
            return Ok(new QuestQueryResponse(
                await questModelFactory.Create(
                    (
                        await questRepository.Collection
                            .Find(filter)
                            .Sort(Builders<Domain.Models.QuestAggregate.Quest>.Sort
                                .Descending(q => q.CreationTime))
                            .Skip(offset)
                            .Limit(configuration.GetValue<int>("Emerald:MediumResponsePackSize"))
                            .ToListAsync()
                        /*
                    await questRepository.GetQueryable()
                        .Where(q => filter.Inject())
                        .OrderByDescending(q => q.CreationTime)
                        .Skip(offset)
                        .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize"))
                        .ToListAsync()
                        )
                        .Select(q => new QuestPairModel(q, q.QuestVersions[0]))
                        .ToList())));
            */
        }

        /// <summary>
        /// Query quests from users the authorized user follows in creationtime order
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet("queryfollowing")]
        public async Task<ActionResult<QuestQueryResponse>> QueryFollowing(
            [FromQuery] int offset,
            [FromServices] ITrackerRepository trackerRepository,
            [FromServices] IQuestRepository questRepository,
            [FromServices] QuestModelFactory questModelFactory,
            [FromServices] IUserService userService)
        {
            User user = await userService.CurrentUser();

            return Ok(new QuestQueryResponse(
                await questModelFactory.Create(
                    (await questRepository.GetQueryable()
                        .Where(q => user.Following.Contains(q.OwnerUserId) &&
                            (q.Public || q.OwnerUserId == user.Id)
                             && q.QuestVersions.Count > 0
                             && q.Locks.Count == 0)
                        .OrderByDescending(q => q.CreationTime)
                        .Skip(offset)
                        .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize"))
                        .ToListAsync())
                        .Select(q => new QuestPairModel(q, q.QuestVersions[0]))
                        .ToList())));
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] List<ObjectId> replayIds,
            [FromServices] ITrackerRepository trackerRepository,
            [FromServices] IQuestRepository questRepository,
            [FromServices] QuestModelFactory questModelFactory)
        {
            List<QuestModel> questModels = new List<QuestModel>();

            foreach (ObjectId replayId in replayIds)
            {
                var quest = await questRepository.Get(replayId);
                questModels.Add(await questModelFactory.Create(
                    new QuestPairModel(quest, quest.GetCurrentQuestVersion())));
            }

            return Ok(questModels);
        }
    }
}
