using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Events
{
    public class QuestFinishDomainEvent : INotification
    {
        public ObjectId QuestId { get; set; }
        public ObjectId UserId { get; set; }

        public QuestFinishDomainEvent(ObjectId questId, ObjectId userId)
        {
            QuestId = questId;
            UserId = userId;
        }
    }
}
