using Emerald.Application.Models;
using Emerald.Application.Models.Quest;
using Emerald.Application.Models.Quest.Tracker;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Domain.Models.TrackerAggregate;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Infrastructure.Repositories;
using Emerald.Infrastructure.ViewModels;
using Emerald.Infrastructure.ViewModelStash;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public class QuestModelFactory : IModelFactory<QuestPairModel, QuestModel>
    {
        private QuestViewModelStash questStash;
        private TrackerModelFactory trackerModelFactory;
        private IUserRepository userRepository;
        private IUserService userService;
        private ITrackerRepository trackerRepository;

        public QuestModelFactory(QuestViewModelStash questStash, TrackerModelFactory trackerModelFactory, IUserRepository userRepository, IUserService userService, ITrackerRepository trackerRepository)
        {
            this.questStash = questStash;
            this.trackerModelFactory = trackerModelFactory;
            this.userRepository = userRepository;
            this.userService = userService;
            this.trackerRepository = trackerRepository;
        }

        public async Task<QuestModel> Create(QuestPairModel questPair, TrackerModel? tracker)
        {
            Quest quest = questPair.Quest;
            QuestVersion? version = questPair.QuestVersion;

            QuestViewModel viewModel = await questStash.Get(quest.Id);
            User owner = await userRepository.Get(quest.OwnerUserId);

            return new QuestModel(
                id: quest.Id.ToString(),
                tracker: tracker,

                ownerId: quest.OwnerUserId,
                ownerName: owner.UserName,
                ownerImageId: owner.ImageId,
                @public: quest.Public,

                approximateTime: version?.ApproximateTime,
                locationName: version?.LocationName,
                location: version == null
                ? null
                : new LocationModel(
                    version.Location.Longitude,
                    version.Location.Latitude),

                title: version?.Title,
                description: version?.Description,
                tags: version?.Tags,
                imageId: version?.ImageId,
                version: version == null ? 1 : version.Version,

                profileImageId: version?.AgentProfileImageId,
                profileName: version?.AgentProfileName,

                creationTime: quest.CreationTime,

                votes: viewModel.Votes,
                plays: viewModel.Plays,
                finishes: viewModel.Finishes);
        }

        public async Task<QuestModel> Create(QuestPairModel questPair)
        {
            User current = await userService.CurrentUser();

            Tracker? tracker = await trackerRepository.FindByUserAndQuest(
                current.Id, questPair.Quest.Id);

            return await Create(
                questPair,
                await trackerModelFactory.CreateNullable(tracker));
        }
    }
}
