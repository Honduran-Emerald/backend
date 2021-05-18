using Emerald.Domain.Models;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Events
{
    public class QuestResponseDomainEvent<ResponseEvent> 
        : INotification where ResponseEvent : IResponseEvent
    {
        public ObjectId TrackerId { get; set; }
        public ObjectId UserId { get; set; }
        public ResponseEvent Event { get; set; }

        public QuestResponseDomainEvent(ObjectId userId, ObjectId trackerId, ResponseEvent responseEvent)
        {
            UserId = userId;
            TrackerId = trackerId;
            Event = responseEvent;
        }
    }
}
