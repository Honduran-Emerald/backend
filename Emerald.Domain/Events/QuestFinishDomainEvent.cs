using MediatR;
using MongoDB.Bson;

namespace Emerald.Domain.Events
{
    public class QuestFinishDomainEvent : INotification
    {
        public ObjectId QuestId { get; set; }
        public ObjectId UserId { get; set; }
        public ObjectId TrackerId { get; set; }

        public QuestFinishDomainEvent(ObjectId questId, ObjectId userId)
        {
            QuestId = questId;
            UserId = userId;
        }
    }
}
