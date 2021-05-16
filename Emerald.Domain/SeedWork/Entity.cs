using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vitamin.Value.Domain.SeedWork
{
    public abstract class Entity
    {
        public abstract ObjectId Id { get; protected set; }

        public Entity()
        {
            Id = ObjectId.GenerateNewId();
        }

        public override bool Equals(object obj)
            => obj is Entity entity
            && Id.Equals(entity.Id);

        public override int GetHashCode()
            => Id.GetHashCode();

        public void ClearEvents()
            => domainEvents.Clear();

        public IReadOnlyCollection<INotification> DomainEvents
            => domainEvents;

        protected void AddDomainEvent(INotification domainEvent)
        {
            if (domainEvents == null)
                domainEvents = new List<INotification>();

            domainEvents.Add(domainEvent);
        }

        [JsonIgnore]
        [BsonIgnore]
        private List<INotification> domainEvents;
    }
}
