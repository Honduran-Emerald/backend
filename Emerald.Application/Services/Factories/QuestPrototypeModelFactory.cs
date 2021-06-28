using Emerald.Application.Models.Prototype;
using Emerald.Application.Models.Quest;
using Emerald.Application.Models.Quest.Tracker;
using Emerald.Domain.Models.PrototypeAggregate;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Domain.Repositories;
using Emerald.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public class QuestPrototypeModelFactory : IModelFactory<Quest, QuestPrototypeModel>
    {
        private IQuestPrototypeRepository questPrototypeRepository;
        private ITrackerRepository trackerRepository;
        private IUserService userService;
        private QuestModelFactory questModelFactory;
        private TrackerModelFactory trackerModelFactory;

        public QuestPrototypeModelFactory(IQuestPrototypeRepository questPrototypeRepository, ITrackerRepository trackerRepository, IUserService userService, QuestModelFactory questModelFactory, TrackerModelFactory trackerModelFactory)
        {
            this.questPrototypeRepository = questPrototypeRepository;
            this.trackerRepository = trackerRepository;
            this.userService = userService;
            this.questModelFactory = questModelFactory;
            this.trackerModelFactory = trackerModelFactory;
        }

        public async Task<QuestPrototypeModel> Create(Quest source)
        {
            User user = await userService.CurrentUser();

            TrackerModel? trackerModel = await trackerModelFactory.CreateNullable(
                await trackerRepository.FindByUserAndQuest(
                    user.Id,
                    source.Id));

            QuestModel? questModel = await questModelFactory.Create(
                   new QuestPairModel(source, source.GetCurrentQuestVersion()),
                   trackerModel);

            QuestPrototype questPrototype = await questPrototypeRepository.Get(source.PrototypeId);

            return new QuestPrototypeModel(
                source.Id,
                questModel,
                questPrototype.Title,
                questPrototype.ImageIdByReference(questPrototype.ImageReference),
                questPrototype.Description,
                questPrototype.Location,
                questPrototype.LocationName,
                questPrototype.AgentProfileName,
                questPrototype.ImageIdByReference(questPrototype.AgentProfileReference),
                source.QuestVersions.Count > 0,
                source.Public,
                true);
        }
    }
}
