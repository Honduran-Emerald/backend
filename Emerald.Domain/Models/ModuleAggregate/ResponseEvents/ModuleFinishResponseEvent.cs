using Emerald.Domain.Events;
using Emerald.Domain.Models.TrackerAggregate;
using MediatR;
using MongoDB.Bson;

namespace Emerald.Domain.Models.ModuleAggregate
{
    public class ModuleFinishResponseEvent : IResponseEvent
    {
        public ObjectId ModuleId { get; set; }

        public ModuleFinishResponseEvent(ObjectId moduleId)
        {
            ModuleId = moduleId;
        }

        public INotification ToDomainEvent(ObjectId userId, ObjectId trackerId)
            => new QuestResponseDomainEvent<ModuleFinishResponseEvent>(userId, trackerId, this);
    }
}
