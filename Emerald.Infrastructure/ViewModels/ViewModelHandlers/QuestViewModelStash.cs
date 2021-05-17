using Emerald.Domain.Models.TrackerAggregate;
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
            return await collection
                .Find(m => m.QuestId == questId)
                .FirstOrDefaultAsync();
        }

        public async Task IncreasePlay(ObjectId questId)
        {
            QuestViewModel model = await collection
                .Find(m => m.QuestId == questId)
                .FirstOrDefaultAsync();

            ++model.Plays;
            await Update(model);
        }

        public async Task IncreaseFinish(ObjectId questId)
        {
            QuestViewModel model = await collection
                .Find(m => m.QuestId == questId)
                .FirstOrDefaultAsync();

            ++model.Finishs;
            await Update(model);
        }

        public async Task IncreaseVote(ObjectId questId, VoteType voteType)
        {
            QuestViewModel model = await collection
                .Find(m => m.QuestId == questId)
                .FirstOrDefaultAsync();

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
