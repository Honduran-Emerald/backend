using Emerald.Domain.Models.PrototypeAggregate;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

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
