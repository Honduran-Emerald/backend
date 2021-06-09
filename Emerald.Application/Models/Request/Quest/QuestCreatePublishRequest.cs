using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Emerald.Application.Models.Request.Quest
{
    public class QuestCreatePublishRequest
    {
        [Required]
        public ObjectId QuestId { get; set; }

        public QuestCreatePublishRequest()
        {
            QuestId = default!;
        }
    }
}
