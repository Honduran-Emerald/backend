using Emerald.Domain.Models.TrackerAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.Tracker
{
    public class TrackerModel
    {
        public ObjectId TrackerId { get; set; }
        public bool NewestQuestVersion { get; set; }
        public bool Finished { get; set; }
        public VoteType Vote { get; set; }
        public DateTime CreationTime { get; set; }

        public TrackerModel(ObjectId trackerId, bool newestQuestVersion, bool finished, VoteType vote, DateTime creationTime)
        {
            TrackerId = trackerId;
            NewestQuestVersion = newestQuestVersion;
            Finished = finished;
            Vote = vote;
            CreationTime = creationTime;
        }

        public TrackerModel()
        {
            NewestQuestVersion = default!;
            Finished = default!;
            Vote = default!;
            CreationTime = default!;
        }
    }
}
