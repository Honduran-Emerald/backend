using MediatR;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Emerald.Domain.SeedWork
{
    public interface IEntity
    {
        ObjectId Id { get; set; }
        void ClearEvents();
        IReadOnlyCollection<INotification> DomainEvents { get; }
    }
}
