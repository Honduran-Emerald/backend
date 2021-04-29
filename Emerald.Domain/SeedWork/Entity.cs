using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Vitamin.Value.Domain.SeedWork
{
    public abstract class Entity
    {
        public abstract long Id { get; protected set; }

        public override bool Equals(object obj)
        {
            return obj is Entity entity &&
                   Id.Equals(entity.Id);
        }

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
