using Emerald.Domain.Events;
using Emerald.Infrastructure.ViewModelStash;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.ViewModels.EventHandler
{
    public class QuestStartDomainEventHandler : INotificationHandler<QuestStartedDomainEvent>
    {
        private QuestViewModelStash stash;

        public QuestStartDomainEventHandler(QuestViewModelStash stash)
        {
            this.stash = stash;
        }

        public async Task Handle(QuestStartedDomainEvent notification, CancellationToken cancellationToken)
        {
            await stash.IncreasePlay(notification.QuestId);
        }
    }
}
