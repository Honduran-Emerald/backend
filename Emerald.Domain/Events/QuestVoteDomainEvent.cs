using Emerald.Domain.Models.TrackerAggregate;
using MediatR;
using MongoDB.Bson;

namespace Emerald.Domain.Events
{
    public class QuestVoteDomainEvent : INotification
    {
        public ObjectId QuestId { get; set; }
        public ObjectId UserId { get; set; }
        public VoteType VoteType { get; set; }

        public QuestVoteDomainEvent(ObjectId questId, ObjectId userId, VoteType voteType)
        {
            QuestId = questId;
            UserId = userId;
            VoteType = voteType;
        }
    }
}
