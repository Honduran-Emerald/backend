using Emerald.Domain.SeedWork;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vitamin.Value.Domain.SeedWork
{
    public class Entity : IEntity
    {
        public ObjectId Id { get; set; }
        public int Test { get; }

        public Entity()
        {
            Id = ObjectId.GenerateNewId();
            domainEvents = new List<INotification>();
        }

        public void GenerateNewIdentifier()
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
            domainEvents.Add(domainEvent);
        }

        [JsonIgnore]
        [BsonIgnore]
        private List<INotification> domainEvents;
    }
}
