using Emerald.Application.Models.Quest;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Response.Quest
{
    public class QuestQueryRequest
    {
        [Required]
        public int Offset { get; set; }

        public ObjectId? OwnerId { get; set; }
    }
}
