using Emerald.Domain.Models.TrackerAggregate;
using Emerald.Infrastructure.ElasticModels;
using Emerald.Infrastructure.ElasticModels.Events;
using Emerald.Infrastructure.ViewModels;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.ViewModelStash
{
    public class QuestViewModelStash
    {
        private IMongoCollection<QuestViewModel> collection;
        private IMediator mediator;

        public QuestViewModelStash(IMongoDbContext context, IMediator mediator)
        {
            collection = context.Emerald.GetCollection<QuestViewModel>("QuestViewModel");
            this.mediator = mediator;
        }

        public async Task<QuestViewModel> Get(ObjectId questId)
        {
            var viewModel = await collection
                .Find(m => m.Id == questId)
                .FirstOrDefaultAsync();

            if (viewModel == null)
            {
                viewModel = new QuestViewModel(questId, 0, 0, 0);
                await collection.InsertOneAsync(viewModel);
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
            ++model.Finishes;
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

        public async Task Update(QuestViewModel model)
        {
            await collection.ReplaceOneAsync(m => m.Id == model.Id, model);
            await mediator.Publish(new QuestElasticModelChangedEvent(model.Id));
        }
    }
}
