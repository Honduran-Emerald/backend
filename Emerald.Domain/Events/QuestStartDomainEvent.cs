using MediatR;
using MongoDB.Bson;

namespace Emerald.Domain.Events
{
    public class QuestStartDomainEvent : INotification
    {
        public ObjectId QuestId { get; set; }
        public ObjectId UserId { get; set; }

        public QuestStartDomainEvent(ObjectId questId, ObjectId userId)
        {
            QuestId = questId;
            UserId = userId;
        }
    }
}
