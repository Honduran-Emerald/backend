using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Emerald.Application.Models.Quest.RequestEvent
{
    public class RequestEventModel
    {
        [Required]
        public ObjectId TrackerId { get; set; }

        public RequestEventModel()
        {
            TrackerId = default!;
        }
    }
}
