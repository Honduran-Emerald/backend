using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.ElasticModels.Events
{
    public class QuestElasticModelChangedEvent : INotification
    {
        public ObjectId QuestId { get; set; }

        public QuestElasticModelChangedEvent(ObjectId questId)
        {
            QuestId = questId;
        }
    }
}
