using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.SeedWork
{
    public interface IEntity
    {
        ObjectId Id { get; set; }
        void ClearEvents();
        IReadOnlyCollection<INotification> DomainEvents { get; }
    }
}
