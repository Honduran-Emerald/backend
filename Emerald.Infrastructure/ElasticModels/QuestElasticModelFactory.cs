using Emerald.Domain.Models.QuestAggregate;
using Emerald.Infrastructure.Repositories;
using Emerald.Infrastructure.Services;
using Emerald.Infrastructure.ViewModelStash;
using MongoDB.Bson;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.ElasticModels
{
    public class QuestElasticModelFactory
    {
        private QuestViewModelStash stash;

        public QuestElasticModelFactory(QuestViewModelStash stash)
        {
            this.stash = stash;
        }

        public async Task<QuestElasticModel> Create(Quest quest)
        {
            var version = quest.GetCurrentQuestVersion();
            var viewModel = await stash.Get(quest.Id);

            return new QuestElasticModel(
                quest.Id.ToString(),
                version?.Title,
                version?.Tags,
                version == null
                ? null
                : new Nest.GeoLocation(
                    version.Location.Latitude,
                    version.Location.Longitude),
                version?.CreationTime,
                viewModel.Votes,
                viewModel.Plays,
                viewModel.Plays == 0 ? 0 :
                    viewModel.Finishes / (float)viewModel.Plays);
        }

        /*
        public async Task Update(ObjectId questId)
        {
            await elasticService.ElasticClient.IndexDocumentOrThrow(
                await questElasticModelFactory.Create(await questRepository.Get(questId)));
        }
        */
    }
}
