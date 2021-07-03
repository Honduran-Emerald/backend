using Emerald.Domain.Models.TrackerAggregate;
using MediatR;
using MongoDB.Bson;

namespace Emerald.Domain.Events
{
    public class QuestVotedDomainEvent : INotification
    {
        public ObjectId QuestId { get; set; }
        public ObjectId UserId { get; set; }
        public VoteType VoteType { get; set; }
        public VoteType PreviousVoteType { get; set; }

        public QuestVotedDomainEvent(ObjectId questId, ObjectId userId, VoteType voteType, VoteType previousVoteType)
        {
            QuestId = questId;
            UserId = userId;
            VoteType = voteType;
            PreviousVoteType = previousVoteType;
        }
    }
}
