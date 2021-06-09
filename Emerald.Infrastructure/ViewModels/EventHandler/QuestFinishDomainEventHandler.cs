using Emerald.Domain.Events;
using Emerald.Infrastructure.ViewModelStash;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.ViewModels.EventHandler
{
    public class QuestFinishDomainEventHandler : INotificationHandler<QuestFinishDomainEvent>
    {
        private QuestViewModelStash stash;

        public QuestFinishDomainEventHandler(QuestViewModelStash stash)
        {
            this.stash = stash;
        }

        public async Task Handle(QuestFinishDomainEvent notification, CancellationToken cancellationToken)
        {
            await stash.IncreaseFinish(notification.QuestId);
        }
    }
}
