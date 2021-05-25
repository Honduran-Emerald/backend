using Emerald.Application.Models;
using Emerald.Application.Models.Quest;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Infrastructure.ViewModelHandlers;
using Emerald.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public class QuestModelFactory : IModelFactory<Quest, QuestModel>
    {
        private QuestViewModelStash questStash;

        public QuestModelFactory(QuestViewModelStash questStash)
        {
            this.questStash = questStash;
        }

        public async Task<QuestModel> Create(Quest quest)
        {
            QuestVersion? version = quest.GetStableQuestVersion();
            QuestViewModel viewModel = await questStash.Get(quest.Id);

            if (version == null)
            {
                throw new NullReferenceException();
            }

            return new QuestModel(
                id: quest.Id.ToString(),
                ownerId: quest.OwnerUserId,

                locationName: "",
                location: new LocationModel(
                    version.Location.Longitude,
                    version.Location.Latitude),

                title: version.Title,
                description: version.Description,
                image: version.Image,
                version: version.Version,

                creationTime: version.CreatedAt,

                votes: viewModel.Votes,
                plays: viewModel.Plays,
                finishs: viewModel.Finishs);
        }
    }
}
