using Emerald.Domain.Events;
using Emerald.Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.DomainEventHandler
{
    public class QuestVersionAddedDomainEventHandler : INotificationHandler<QuestVersionAddedDomainEvent>
    {
        private IImageIndexService imageIndexService;

        public QuestVersionAddedDomainEventHandler(IImageIndexService imageIndexService)
        {
            this.imageIndexService = imageIndexService;
        }

        public async Task Handle(QuestVersionAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            if (notification.QuestVersion.ImageId != null)
            {
                await imageIndexService.IncreaseImageReference(notification.QuestVersion.ImageId);
            }
        }
    }
}
