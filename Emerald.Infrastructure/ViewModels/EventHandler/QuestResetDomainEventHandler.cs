using Emerald.Domain.Events;
using Emerald.Domain.Models.TrackerAggregate;
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
    public class QuestResetDomainEventHandler : INotificationHandler<QuestResetDomainEvent>
    {
        public QuestViewModelStash stash;

        public QuestResetDomainEventHandler(QuestViewModelStash stash)
        {
            this.stash = stash;
        }

        public async Task Handle(QuestResetDomainEvent notification, CancellationToken cancellationToken)
        {
            QuestViewModel model = await stash.Get(notification.QuestId);

            if (notification.Vote != VoteType.None)
            {
                model.Votes += notification.Vote == VoteType.Up ? -1 : 1;
            }

            if (notification.Finished)
            {
                model.Finishes -= 1;
            }

            await stash.Update(model);
        }
    }
}
