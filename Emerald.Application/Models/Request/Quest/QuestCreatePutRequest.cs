using Emerald.Domain.Models.PrototypeAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Request.Quest
{
    public class QuestCreatePutRequest
    {
        [Required]
        public ObjectId QuestId { get; set; }

        [Required]
        public QuestPrototype QuestPrototype { get; set; }

        private QuestCreatePutRequest()
        {
            QuestId = default!;
            QuestPrototype = default!;
        }
    }
}
