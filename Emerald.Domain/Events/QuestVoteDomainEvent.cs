using Emerald.Domain.Models.TrackerAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Events
{
    public class QuestVoteDomainEvent
    {
        public ObjectId QuestId { get; set; }
        public ObjectId UserId { get; set; }
        public VoteType VoteType { get; set; }

        public QuestVoteDomainEvent(ObjectId questId, ObjectId userId, VoteType voteType)
        {
            QuestId = questId;
            UserId = userId;
            VoteType = voteType;
        }
    }
}
