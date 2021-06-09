using Emerald.Domain.Models.TrackerAggregate;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

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
