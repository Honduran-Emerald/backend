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
    public class QuestVoteDomainEventHandler : INotificationHandler<QuestVoteDomainEvent>
    {
        private QuestViewModelStash stash;

        public QuestVoteDomainEventHandler(QuestViewModelStash stash)
        {
            this.stash = stash;
        }

        public async Task Handle(QuestVoteDomainEvent notification, CancellationToken cancellationToken)
        {
            await stash.IncreaseVote(notification.QuestId, notification.VoteType);
        }
    }
}
