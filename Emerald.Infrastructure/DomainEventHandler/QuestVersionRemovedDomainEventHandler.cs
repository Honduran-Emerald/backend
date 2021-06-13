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
    public class QuestVersionRemovedDomainEventHandler : INotificationHandler<QuestVersionRemovedDomainEvent>
    {
        private IImageIndexService imageIndexService;

        public QuestVersionRemovedDomainEventHandler(IImageIndexService imageIndexService)
        {
            this.imageIndexService = imageIndexService;
        }

        public async Task Handle(QuestVersionRemovedDomainEvent notification, CancellationToken cancellationToken)
        {
            if (notification.QuestVersion.ImageId != null)
            {
                await imageIndexService.DecreaseImageReference(notification.QuestVersion.ImageId);
            }
        }
    }
}
