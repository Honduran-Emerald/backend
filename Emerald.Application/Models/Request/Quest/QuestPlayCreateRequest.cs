using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
