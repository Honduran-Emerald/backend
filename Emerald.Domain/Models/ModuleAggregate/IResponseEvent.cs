using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models
{
    public interface IResponseEvent
    {
        INotification ToDomainEvent(ObjectId userId, ObjectId trackerId);
    }
}
