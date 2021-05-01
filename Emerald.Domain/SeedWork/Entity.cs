using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Vitamin.Value.Domain.SeedWork
{
    public abstract class Entity
    {
        public abstract ObjectId Id { get; protected set; }

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

        private List<INotification> domainEvents;
    }
}
