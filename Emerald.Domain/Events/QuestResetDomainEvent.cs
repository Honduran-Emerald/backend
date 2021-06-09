using Emerald.Domain.Models.TrackerAggregate;
using MediatR;
using MongoDB.Bson;

namespace Emerald.Domain.Events
{
    public class QuestResetDomainEvent : INotification
    {
        public ObjectId QuestId { get; set; }
        public VoteType Vote { get; set; }
        public bool Finished { get; set; }

        public QuestResetDomainEvent(ObjectId questId, VoteType vote, bool finished)
        {
            QuestId = questId;
            Vote = vote;
            Finished = finished;
        }
    }
}
