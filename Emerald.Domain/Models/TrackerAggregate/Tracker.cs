using Emerald.Domain.Events;
using Emerald.Domain.Models.LockAggregate;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.TrackerAggregate
{
    public class Tracker : LockableEntity
    {
        public ObjectId UserId { get; set; }

        public ObjectId QuestId { get; set; }
        public int QuestVersion { get; set; }

        public int ExperienceCollected { get; set; }
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

        public void IncreaseExperience(int experience)
        {
            ExperienceCollected += experience;
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
            var previous = Vote;
            Vote = VoteType.Up;
            AddDomainEvent(new QuestVotedDomainEvent(QuestId, UserId, Vote, previous));
        }

        public void Downvote()
        {
            var previous = Vote;
            Vote = VoteType.Down;
            AddDomainEvent(new QuestVotedDomainEvent(QuestId, UserId, Vote, previous));
        }

        public void Unvote()
        {
            var previous = Vote;
            Vote = VoteType.None;
            AddDomainEvent(new QuestVotedDomainEvent(QuestId, UserId, Vote, previous));
        }

        public void Finish()
        {
            Finished = true;
            AddDomainEvent(new QuestFinishDomainEvent(QuestId, UserId));
        }

        public void Reset(int questVersion)
        {
            ExperienceCollected = 0;
            CreatedAt = DateTime.UtcNow;
            QuestVersion = questVersion;

            Nodes.Clear();
            AddDomainEvent(new QuestResetDomainEvent(QuestId, Vote, Finished));
            Finished = false;
            Vote = VoteType.None;
        }
    }
}
