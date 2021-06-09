using Emerald.Domain.Events;
using MediatR;
using MongoDB.Bson;

namespace Emerald.Domain.Models.ModuleAggregate.ResponseEvents
{
    public class QuestFinishResponseEvent : IResponseEvent
    {
        public float FinishFactor { get; set; }

        public QuestFinishResponseEvent(float finishFactor)
        {
            FinishFactor = finishFactor;
        }

        public INotification ToDomainEvent(ObjectId userId, ObjectId trackerId)
            => new QuestResponseDomainEvent<QuestFinishResponseEvent>(userId, trackerId, this);
    }
}
