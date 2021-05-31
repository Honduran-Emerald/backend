using Emerald.Domain.Events;
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
        public ObjectId UserId { get; set; }

        public ObjectId QuestId { get; set; }
        public int QuestVersion { get; set; }

        public VoteType Vote { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<TrackerNode> Nodes { get; set; }
        public bool Finished { get; set; }

        public Tracker(ObjectId userId, Quest quest, QuestVersion questVersion)
            : this()
        {
            QuestId = quest.Id;
            QuestVersion = questVersion.Version;

            UserId = userId;
            CreatedAt = DateTime.UtcNow;
            Finished = false;

            AddDomainEvent(new QuestStartedDomainEvent(QuestId, UserId));
        }

        private Tracker()
        {
            Nodes = new List<TrackerNode>();
        }

        public void AddTrackerPath(TrackerNode trackerPath)
        {
            Nodes.Add(trackerPath);
        }

        public TrackerNode GetCurrentTrackerPath()
        {
            return Nodes[Nodes.Count - 1];
        }

        public void Upvote()
        {
            if (Vote != VoteType.None)
            {
                throw new DomainException("Quest already voted");
            }

            Vote = VoteType.Up;
            AddDomainEvent(new QuestVotedDomainEvent(QuestId, UserId, Vote));
        }

        public void Downvote()
        {
            if (Vote != VoteType.None)
            {
                throw new DomainException("Quest already voted");
            }

            Vote = VoteType.Down;
            AddDomainEvent(new QuestVotedDomainEvent(QuestId, UserId, Vote));
        }

        public void Finish()
        {
            Finished = true;
            AddDomainEvent(new QuestFinishDomainEvent(QuestId, UserId));
        }

        public void Reset()
        {
            TrackerNode firstNode = Nodes[0];
            Nodes.Clear();
            Nodes.Add(firstNode);

            AddDomainEvent(new QuestResetDomainEvent(QuestId, Vote, Finished));
            Finished = false;
            Vote = VoteType.None;
        }
    }
}
