using Emerald.Domain.Models.LockAggregate;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.TrackerAggregate;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Domain.Repositories;
using Emerald.Domain.SeedWork;
using Emerald.Infrastructure.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Services
{
    public class LockService
    {
        private ILockRepository lockRepository;
        private IQuestRepository questRepository;
        private IUserRepository userRepository;
        private ITrackerRepository trackerRepository;

        public LockService(ILockRepository lockRepository, IQuestRepository questRepository, IUserRepository userRepository, ITrackerRepository trackerRepository)
        {
            this.lockRepository = lockRepository;
            this.questRepository = questRepository;
            this.userRepository = userRepository;
            this.trackerRepository = trackerRepository;
        }

        public async Task Lock(User user, Lock @lock)
        {
            @lock.Users.Add(user.Id);
            @lock.Quests.AddRange(user.QuestIds);

            user.Locks.Add(@lock);

            foreach (ObjectId questId in user.QuestIds)
            {
                Quest quest = await questRepository.Get(questId);
                quest.Locks.Add(@lock);
                await questRepository.Update(quest);
            }

            await userRepository.Update(user);
            await lockRepository.Add(@lock);
        }

        public async Task Lock(Quest quest, Lock @lock)
        {
            @lock.Quests.Add(quest.Id);
            quest.Locks.Add(@lock);

            var trackers = await trackerRepository.GetQueryable()
                .Where(t => t.QuestId == quest.Id)
                .ToListAsync();

            foreach (Tracker tracker in trackers)
            {
                @lock.Trackers.Add(tracker.Id);
                tracker.Locks.Add(@lock);
                await trackerRepository.Update(tracker);
            }

            await questRepository.Update(quest);
            await lockRepository.Add(@lock);
        }

        public async Task RemoveLock(Lock @lock)
        {
            foreach (ObjectId userId in @lock.Users)
            {
                User user = await userRepository.Get(userId);
                user.Locks.RemoveAt(user.Locks.FindIndex(l => l.Id == @lock.Id));
                await userRepository.Update(user);
            }

            foreach (ObjectId questId in @lock.Quests)
            {
                Quest quest = await questRepository.Get(questId);
                quest.Locks.RemoveAt(quest.Locks.FindIndex(l => l.Id == @lock.Id));
                await questRepository.Update(quest);
            }

            foreach (ObjectId trackerId in @lock.Trackers)
            {
                Tracker tracker = await trackerRepository.Get(trackerId);
                tracker.Locks.RemoveAt(tracker.Locks.FindIndex(l => l.Id == @lock.Id));
                await trackerRepository.Update(tracker);
            }

            await lockRepository.Remove(@lock.Id);
        }
    }
}
