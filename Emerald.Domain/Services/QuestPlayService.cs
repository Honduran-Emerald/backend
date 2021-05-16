using Emerald.Domain.Models;
using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.ModuleAggregate.RequestEvents;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.TrackerAggregate;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Domain.Repositories;
using Emerald.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Services
{
    public class QuestPlayService
    {
        private IModuleRepository moduleRepository;
        private ITrackerRepository trackerRepository;

        // representing a user playing a quest and handling
        // requestevents
        public QuestPlayService(IModuleRepository moduleRepository, ITrackerRepository trackerRepository)
        {
            this.moduleRepository = moduleRepository;
            this.trackerRepository = trackerRepository;
        }

        public async Task<ResponseEvent> HandleEvent(User user, RequestEvent requestEvent)
        {
            if (user.ActiveTrackerId == null)
            {
                throw new DomainException("Can not handle event without active tracker");
            }

            Tracker tracker = await trackerRepository.Get(user.ActiveTrackerId.Value);
            TrackerPath path = tracker.GetCurrentTrackerPath();
            Module module = await moduleRepository.Get(path.ModuleId);

            ResponseEvent responseEvent = module.ProcessEvent(path.Memento, requestEvent);
            path.UpdateMemento(responseEvent.Memento);

            if (responseEvent is NextModuleResponseEvent nextModuleEvent)
            {
                tracker.Path.Add(
                    new TrackerPath(nextModuleEvent.ModuleId));
            }

            await trackerRepository.Update(tracker);

            return responseEvent;
        }

        private async Task HandleChoiceEvent()
        {
        }
    }
}
