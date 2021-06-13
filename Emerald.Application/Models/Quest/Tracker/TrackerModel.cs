﻿using Emerald.Domain.Models.TrackerAggregate;
using MongoDB.Bson;
using System;

namespace Emerald.Application.Models.Quest.Tracker
{
    public class TrackerModel
    {
        public ObjectId QuestId { get; set; }
        public ObjectId TrackerId { get; set; }

        public bool NewestQuestVersion { get; set; }
        public bool Finished { get; set; }
        public VoteType Vote { get; set; }
        public DateTime CreationTime { get; set; }

        public string QuestName { get; set; }

        public string AgentProfileImageId { get; set; }
        public string AgentProfileName { get; set; }

        public string Objective { get; set; }
        public string Author { get; set; }

        public TrackerModel(ObjectId questId, ObjectId trackerId, bool newestQuestVersion, bool finished, VoteType vote, DateTime creationTime, string questName, string agentProfileImageId, string agentProfileName, string objective, string author)
        {
            QuestId = questId;
            TrackerId = trackerId;
            NewestQuestVersion = newestQuestVersion;
            Finished = finished;
            Vote = vote;
            CreationTime = creationTime;
            QuestName = questName;
            AgentProfileImageId = agentProfileImageId;
            AgentProfileName = agentProfileName;
            Objective = objective;
            Author = author;
        }

        public TrackerModel()
        {
            NewestQuestVersion = default!;
            Finished = default!;
            Vote = default!;
            CreationTime = default!;
            QuestName = default!;
            Objective = default!;
            AgentProfileImageId = default!;
            AgentProfileName = default!;
            Author = default!;
        }
    }
}
