using Emerald.Domain.Events;
using MediatR;
using MongoDB.Bson;

namespace Emerald.Domain.Models.ModuleAggregate.ResponseEvents
{
    public class ExperienceResponseEvent : IResponseEvent
    {
        public long Experience { get; set; }

        public ExperienceResponseEvent(long experience)
        {
            Experience = experience;
        }

        public INotification ToDomainEvent(ObjectId userId, ObjectId trackerId)
            => new QuestResponseDomainEvent<ExperienceResponseEvent>(userId, trackerId, this);
    }
}
