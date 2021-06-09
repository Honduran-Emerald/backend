using Emerald.Domain.Events;
using Emerald.Infrastructure.ViewModelStash;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.ViewModels.EventHandler
{
    public class QuestVoteDomainEventHandler : INotificationHandler<QuestVotedDomainEvent>
    {
        private QuestViewModelStash stash;

        public QuestVoteDomainEventHandler(QuestViewModelStash stash)
        {
            this.stash = stash;
        }

        public async Task Handle(QuestVotedDomainEvent notification, CancellationToken cancellationToken)
        {
            await stash.IncreaseVote(notification.QuestId, notification.VoteType);
        }
    }
}
