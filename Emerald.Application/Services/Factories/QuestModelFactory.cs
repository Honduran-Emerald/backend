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
            QuestVersion version = quest.GetStableQuestVersion();
            QuestViewModel viewModel = await questStash.Get(quest.Id);

            return new QuestModel(
                quest.Id.ToString(),
                quest.OwnerUserId,

                new LocationModel
                {
                    Latitude = version.Location.Latitude,
                    Longitude = version.Location.Longitude
                },

                version.Title,
                version.Description,
                version.Image,
                version.Version,

                version.CreatedAt,

                viewModel.Votes,
                viewModel.Plays,
                viewModel.Finishs);
        }
    }
}
