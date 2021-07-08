using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.ModuleAggregate.ResponseEvents
{
    public class FailureResponseEvent : IResponseEvent
    {
        public bool Close { get; set; }

        public FailureResponseEvent(bool close)
        {
            Close = close;
        }

        public INotification? ToDomainEvent(ObjectId userId, ObjectId trackerId)
        {
            return null;
        }
    }
}
