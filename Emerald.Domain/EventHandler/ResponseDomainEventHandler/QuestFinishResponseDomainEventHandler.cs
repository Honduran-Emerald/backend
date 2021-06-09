using Emerald.Domain.Events;
using Emerald.Domain.Models.ModuleAggregate.ResponseEvents;
using Emerald.Domain.Models.TrackerAggregate;
using Emerald.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Emerald.Domain.EventHandler.ResponseDomainEventHandler
{
    public class QuestFinishResponseDomainEventHandler
        : INotificationHandler<QuestResponseDomainEvent<QuestFinishResponseEvent>>
    {
        private ITrackerRepository trackerRepository;

        public QuestFinishResponseDomainEventHandler(ITrackerRepository trackerRepository)
        {
            this.trackerRepository = trackerRepository;
        }

        public async Task Handle(QuestResponseDomainEvent<QuestFinishResponseEvent> notification, CancellationToken cancellationToken)
        {
            Tracker tracker = await trackerRepository.Get(notification.TrackerId);
            tracker.Finish();
            await trackerRepository.Update(tracker);
        }
    }
}
