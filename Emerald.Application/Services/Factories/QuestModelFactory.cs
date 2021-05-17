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
    public class QuestModelFactory : IQuestModelFactory
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

            return new QuestModel
            {
                Id = quest.Id.ToString(),
                OwnerId = quest.OwnerUserId,

                Title = version.Title,
                Description = version.Description,
                Image = version.Image,
                Version = version.Version,

                CreatedAt = version.CreatedAt,
                
                Votes = viewModel.Votes,
                Plays = viewModel.Plays,
                Finishs = viewModel.Finishs
            };
        }
    }
}
