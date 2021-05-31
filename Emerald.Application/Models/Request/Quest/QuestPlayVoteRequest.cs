using Emerald.Domain.Models.TrackerAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Request.Quest
{
    public class QuestPlayVoteRequest
    {
        [Required]
        public ObjectId TrackerId { get; set; }

        [Required]
        public VoteType Vote { get; set; }

        public QuestPlayVoteRequest()
        {
            Vote = default!;
        }
    }
}
