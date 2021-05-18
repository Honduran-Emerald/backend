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
    public class ExperienceResponseEvent : IResponseEvent
    {
        public long Experience { get; set; }

        public ExperienceResponseEvent(long experience)
        {
            Experience = experience;
        }

        public INotification ToDomainEvent(ObjectId userId, ObjectId trackerId)
        {
            return new QuestResponseDomainEvent<ExperienceResponseEvent>(
                userId, trackerId);
        }
    }
}
