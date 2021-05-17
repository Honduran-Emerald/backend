using Emerald.Domain.Events;
using Emerald.Infrastructure.ViewModelHandlers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.ViewModels.EventHandler
{
    public class QuestStartDomainEventHandler : INotificationHandler<QuestStartDomainEvent>
    {
        private QuestViewModelStash stash;

        public QuestStartDomainEventHandler(QuestViewModelStash stash)
        {
            this.stash = stash;
        }

        public async Task Handle(QuestStartDomainEvent notification, CancellationToken cancellationToken)
        {
            await stash.IncreasePlay(notification.QuestId);
        }
    }
}
