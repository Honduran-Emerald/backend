using Emerald.Application.Models;
using Emerald.Application.Models.Quest;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Infrastructure.Repositories;
using Emerald.Infrastructure.ViewModelHandlers;
using Emerald.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public class QuestModelFactory : IModelFactory<KeyValuePair<Quest, int>, QuestModel>
    {
        private QuestViewModelStash questStash;
        private IUserRepository userRepository;

        public QuestModelFactory(QuestViewModelStash questStash, IUserRepository userRepository)
        {
            this.questStash = questStash;
            this.userRepository = userRepository;
        }

        public async Task<QuestModel> Create(KeyValuePair<Quest, int> questPair)
        {
            Quest quest = questPair.Key;
            QuestVersion? version = quest.GetQuestVersion(questPair.Value);

            if (version == null)
            {
                throw new NullReferenceException();
            }

            QuestViewModel viewModel = await questStash.Get(quest.Id);
            User user = await userRepository.Get(quest.OwnerUserId);

            return new QuestModel(
                id: quest.Id.ToString(),

                ownerId: quest.OwnerUserId,
                ownerName: user.UserName,
                ownerImageId: user.ImageId,

                @public: version.Public,
                
                locationName: "Darmstadt",
                location: new LocationModel(
                    version.Location.Longitude,
                    version.Location.Latitude),

                title: version.Title,
                description: version.Description,
                tags: version.Tags,
                imageId: version.ImageId,
                version: version.Version,

                profileImageId: version.ProfileImageId,
                profileName: version.ProfileName,

                creationTime: version.CreationTime,

                votes: viewModel.Votes,
                plays: viewModel.Plays,
                finishes: viewModel.Finishes);
        }
    }
}
