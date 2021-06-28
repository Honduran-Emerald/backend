using MongoDB.Bson;
using System;
using System.ComponentModel.DataAnnotations;

namespace Emerald.Application.Models.Response.Quest
{
    public class QuestQueryRequest
    {
        [Required]
        public int Offset { get; set; } = 0;

        public string? Search { get; set; }

        public LocationModel? Location { get; set; }
        public float? Radius { get; set; }
        public DateTime? PreferAfter { get; set; }

        public ObjectId? OwnerId { get; set; }
    }
}
