using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Emerald.Application.Models.Request.Quest
{
    public class QuestPlayCreateRequest
    {
        [Required]
        public ObjectId QuestId { get; set; }

        public QuestPlayCreateRequest()
        {
            QuestId = default!;
        }
    }
}
