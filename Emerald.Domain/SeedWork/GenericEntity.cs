using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Emerald.Domain.SeedWork
{
    public class GenericEntity<T> : IEntity<T>
    {
        public T Id { get; set; }

        public GenericEntity(T id)
        {
            Id = id;
            domainEvents = new List<INotification>();
        }

        public override bool Equals(object? obj)
            => obj is GenericEntity<T> entity
            && Id!.Equals(entity!.Id);

        public override int GetHashCode()
            => Id!.GetHashCode();

        public void ClearEvents()
            => domainEvents.Clear();

        [JsonIgnore]
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
