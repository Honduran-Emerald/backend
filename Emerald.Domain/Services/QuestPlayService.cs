using Emerald.Domain.Models;
using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.TrackerAggregate;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Domain.Repositories;
using Emerald.Infrastructure.Repositories;
using MediatR;
using MongoDB.Bson;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Services
{
    public class QuestPlayService
    {
        private IModuleRepository moduleRepository;
        private ITrackerRepository trackerRepository;
        private IMediator mediator;

        public QuestPlayService(IModuleRepository moduleRepository, ITrackerRepository trackerRepository, IMediator mediator)
        {
            this.moduleRepository = moduleRepository;
            this.trackerRepository = trackerRepository;
            this.mediator = mediator;
        }
        
        public async Task<ResponseEventCollection> HandleEvent(User user, Tracker tracker, RequestEvent requestEvent)
        {
            if (user.TrackerIds.Contains(tracker.Id) == false)
            {
                throw new DomainException($"Tracker '{tracker.Id}' for user '{user.UserName}' not found");
            }

            if (tracker.Finished)
            {
                throw new DomainException("Tracker is already finished");
            }

            TrackerNode path = tracker.GetCurrentTrackerPath();
            Module module = await moduleRepository.Get(path.ModuleId);

            ResponseEventCollection responseEventCollection = module.ProcessEvent(
                path.Memento,
                requestEvent);
            path.UpdateMemento(responseEventCollection.Memento);
            await trackerRepository.Update(tracker);

            foreach (IResponseEvent responseEvent in responseEventCollection.Events)
            {
                INotification? domainEvent = responseEvent.ToDomainEvent(user.Id, tracker.Id);

                if (domainEvent != null)
                {
                    await mediator.Publish(domainEvent);
                }
            }

            return responseEventCollection;
        }
    }
}
