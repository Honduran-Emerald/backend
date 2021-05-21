using MediatR;
using MongoDB.Bson;

namespace Emerald.Domain.Events
{
    public class QuestStartedDomainEvent : INotification
    {
        public ObjectId QuestId { get; set; }
        public ObjectId UserId { get; set; }

        public QuestStartedDomainEvent(ObjectId questId, ObjectId userId)
        {
            QuestId = questId;
            UserId = userId;
        }
    }
}
