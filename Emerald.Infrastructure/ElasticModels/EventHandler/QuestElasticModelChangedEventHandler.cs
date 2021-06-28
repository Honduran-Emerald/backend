using Emerald.Infrastructure.ElasticModels.Events;
using Emerald.Infrastructure.Repositories;
using Emerald.Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.ElasticModels.EventHandler
{
    public class QuestElasticModelChangedEventHandler : INotificationHandler<QuestElasticModelChangedEvent>
    {
        private QuestElasticModelFactory questElasticModelFactory;
        private IQuestRepository questRepository;
        private IElasticService elasticService;

        public QuestElasticModelChangedEventHandler(QuestElasticModelFactory questElasticModelFactory, IQuestRepository questRepository, IElasticService elasticService)
        {
            this.questElasticModelFactory = questElasticModelFactory;
            this.questRepository = questRepository;
            this.elasticService = elasticService;
        }

        public async Task Handle(QuestElasticModelChangedEvent notification, CancellationToken cancellationToken)
        {
            await elasticService.ElasticClient.IndexDocumentOrThrow(
                await questElasticModelFactory.Create(
                    await questRepository.Get(notification.QuestId)));
        }
    }
}
