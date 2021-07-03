using Emerald.Domain.Events;
using Emerald.Domain.Models.TrackerAggregate;
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
            switch (notification.PreviousVoteType)
            {
                case VoteType.Down:
                    await stash.IncreaseVote(notification.QuestId, VoteType.Up);
                    break;
                case VoteType.Up:
                    await stash.IncreaseVote(notification.QuestId, VoteType.Down);
                    break;
            }

            await stash.IncreaseVote(notification.QuestId, notification.VoteType);
        }
    }
}
