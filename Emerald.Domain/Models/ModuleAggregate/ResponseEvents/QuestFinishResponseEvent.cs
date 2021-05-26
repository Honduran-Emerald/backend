using Emerald.Domain.Events;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.ModuleAggregate.ResponseEvents
{
    public class QuestFinishResponseEvent : IResponseEvent
    {
        public float FinishFactor { get; set; }

        public QuestFinishResponseEvent(float finishFactor)
        {
            FinishFactor = finishFactor;
        }

        public INotification ToDomainEvent(ObjectId userId, ObjectId trackerId)
            => new QuestResponseDomainEvent<QuestFinishResponseEvent>(userId, trackerId, this);
    }
}
