using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.TrackerAggregate
{
    public class Tracker : Entity
    {
        public ObjectId UserId { get; private set; }

        public ObjectId QuestId { get; private set; }
        public int QuestVersion { get; private set; }

        public VoteType Vote { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public List<TrackerNode> Path { get; private set; }
        public bool Finished { get; private set; }

        public Tracker(ObjectId userId, Quest quest, QuestVersion questVersion)
            : this()
        {
            QuestId = quest.Id;
            QuestVersion = questVersion.Version;

            UserId = userId;
            CreatedAt = DateTime.UtcNow;
            Finished = false;
        }

        private Tracker()
        {
            Path = new List<TrackerNode>();
        }

        public void AddTrackerPath(TrackerNode trackerPath)
        {
            Path.Add(trackerPath);
        }

        public TrackerNode GetCurrentTrackerPath()
        {
            return Path[Path.Count - 1];
        }

        public void Upvote()
        {
            if (Vote != VoteType.None)
            {
                throw new DomainException("Quest already voted");
            }

            Vote = VoteType.Up;
        }

        public void Downvote()
        {
            if (Vote != VoteType.None)
            {
                throw new DomainException("Quest already voted");
            }

            Vote = VoteType.Down;
        }

        public void Finish()
        {
            Finished = true;
        }
    }
}
