using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
