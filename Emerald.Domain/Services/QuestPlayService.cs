using Emerald.Domain.Models;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.TrackerAggregate;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Services
{
    public class QuestPlayService
    {
        private IQuestVersionRepository questVersionRepository;
        private ITrackerRepository trackerRepository;

        // representing a user playing a quest and handling
        // requestevents
        public QuestPlayService(IQuestVersionRepository questVersionRepository, ITrackerRepository trackerRepository)
        {
            this.questVersionRepository = questVersionRepository;
            this.trackerRepository = trackerRepository;
        }

        public void HandleEvent(User user, RequestEvent requestEvent)
        {
            // 1. get current tracker
            // 2. get quest for tracker
            // 3. process event
        }

        public void SelectQuest(User user, Quest quest)
        {
        }
    }
}
