﻿using Emerald.Domain.Models.TrackerAggregate;
using Emerald.Infrastructure.ViewModels;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.ViewModelHandlers
{
    public class QuestViewModelStash
    {
        private IMongoCollection<QuestViewModel> collection;

        public QuestViewModelStash(IMongoDbContext context)
        {
            collection = context.Emerald.GetCollection<QuestViewModel>("questviewmodel");
        }

        public async Task<QuestViewModel> Get(ObjectId questId)
        {
            var viewModel = await collection
                .Find(m => m.QuestId == questId)
                .FirstOrDefaultAsync();

            if (viewModel == null)
            {
                viewModel = new QuestViewModel(questId, 0, 0, 0);
            }

            return viewModel;
        }

        public async Task IncreasePlay(ObjectId questId)
        {
            QuestViewModel model = await Get(questId);

            ++model.Plays;
            await Update(model);
        }

        public async Task IncreaseFinish(ObjectId questId)
        {
            QuestViewModel model = await Get(questId);
            ++model.Finishs;
            await Update(model);
        }

        public async Task IncreaseVote(ObjectId questId, VoteType voteType)
        {
            QuestViewModel model = await Get(questId);

            if (voteType == VoteType.Up)
            {
                ++model.Votes;
            }
            else
            {
                --model.Votes;
            }

            await Update(model);
        }

        private async Task Update(QuestViewModel model)
        {
            await collection.ReplaceOneAsync(m => m.QuestId == model.QuestId, model);
        }
    }
}
