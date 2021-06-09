using Emerald.Domain.Events;
using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.TrackerAggregate;
using Emerald.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Emerald.Domain.EventHandler
{
    class ModuleFinishResponseDomainEventHandler
        : INotificationHandler<QuestResponseDomainEvent<ModuleFinishResponseEvent>>
    {
        private ITrackerRepository trackerRepository;

        public ModuleFinishResponseDomainEventHandler(ITrackerRepository trackerRepository)
        {
            this.trackerRepository = trackerRepository;
        }

        public async Task Handle(QuestResponseDomainEvent<ModuleFinishResponseEvent> notification, CancellationToken cancellationToken)
        {
            Tracker tracker = await trackerRepository.Get(notification.TrackerId);
            tracker.AddTrackerPath(new TrackerNode(notification.Event.ModuleId));
            await trackerRepository.Update(tracker);
        }
    }
}
