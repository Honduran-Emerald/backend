using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
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
            Path.Add(new TrackerPath(questVersion.FirstModule));

            CreatedAt = DateTime.UtcNow;
        }

        private Tracker()
        {
            Path = new List<TrackerPath>();
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
