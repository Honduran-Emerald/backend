using Emerald.Domain.Models.PrototypeAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Response.Quest
{
    public class QuestCreateCreateResponse
    {
        public ObjectId QuestId { get; set; }
        public QuestPrototype QuestPrototype { get; set; }

        public QuestCreateCreateResponse(ObjectId questId, QuestPrototype questPrototype)
        {
            QuestId = questId;
            QuestPrototype = questPrototype;
        }
    }
}
