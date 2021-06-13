using Emerald.Domain.SeedWork;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vitamin.Value.Domain.SeedWork
{
    public class Entity : GenericEntity<ObjectId>
    {
        public ObjectId Id { get; set; }
        
        public Entity(ObjectId id)
            : base(id)
        {
        }

        public Entity()
        {
            Id = ObjectId.GenerateNewId();
            domainEvents = new List<INotification>();
        }

        public override bool Equals(object? obj)
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
        }
    }
}
