using Emerald.Application.Models.Quest.Tracker;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Emerald.Application.Models.Quest
{
    public class QuestModel
    {
        public string Id { get; set; }
        public ObjectId OwnerId { get; set; }

        public TrackerModel? Tracker { get; set; }

        public string OwnerName { get; set; }
        public string? OwnerImageId { get; set; }

        public bool Public { get; set; }
        public int Version { get; set; }

        public string? ApproximateTime { get; set; }
        public string? LocationName { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<string>? Tags { get; set; }

        public LocationModel? Location { get; set; }
        public string? ImageId { get; set; }

        public string? AgentProfileImageId { get; set; }
        public string? AgentProfileName { get; set; }

        public DateTime CreationTime { get; set; }

        public int Votes { get; set; }
        public int Plays { get; set; }
        public int Finishes { get; set; }

        public QuestModel(string id, ObjectId ownerId, TrackerModel? tracker, string ownerName, string? ownerImageId, bool @public, int version, string? approximateTime, string? locationName, string? title, string? description, List<string>? tags, LocationModel? location, string? imageId, string? profileImageId, string? profileName, DateTime creationTime, int votes, int plays, int finishes)
        {
            Id = id;
            OwnerId = ownerId;
            Tracker = tracker;
            OwnerName = ownerName;
            OwnerImageId = ownerImageId;
            Public = @public;
            Version = version;
            ApproximateTime = approximateTime;
            LocationName = locationName;
            Title = title;
            Description = description;
            Tags = tags;
            Location = location;
            ImageId = imageId;
            AgentProfileImageId = profileImageId;
            AgentProfileName = profileName;
            CreationTime = creationTime;
            Votes = votes;
            Plays = plays;
            Finishes = finishes;
        }

        private QuestModel()
        {
            Id = default!;
            OwnerId = default!;
            OwnerId = default!;
            OwnerName = default!;
            OwnerImageId = default!;
            Public = default!;
            Version = default!;
            LocationName = default!;
            Title = default!;
            Description = default!;
            Tags = default!;
            Location = default!;
            ImageId = default!;
            AgentProfileImageId = default!;
            AgentProfileName = default!;
            CreationTime = default!;
            Votes = default!;
            Plays = default!;
            Finishes = default!;
        }
    }
}
