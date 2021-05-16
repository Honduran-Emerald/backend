using Emerald.Domain.Models.QuestVersionAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.TrackerAggregate
{
    public class Tracker : Entity
    {
        public override ObjectId Id { get; protected set; }

        public ObjectId QuestVersionId { get; private set; }
        public VoteType Vote { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public List<TrackerPath> Path { get; private set; }

        public Tracker(QuestVersion questVersion)
            : this()
        {
            QuestVersionId = questVersion.Id;
            CreatedAt = DateTime.UtcNow;
        }

        private Tracker()
        {
            Path = new List<TrackerPath>();
        }

        public void AddTrackerPath(TrackerPath trackerPath)
        {
            Path.Add(trackerPath);
        }

        public TrackerPath GetCurrentTrackerPath()
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
    }
}
