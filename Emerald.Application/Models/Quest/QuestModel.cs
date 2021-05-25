using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Domain.Models.TrackerAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest
{
    public class QuestModel
    {
        public string Id { get; set; }
        public ObjectId OwnerId { get; set; }

        public string OwnerName { get; set; }
        public string? OwnerImageId { get; set; }

        public bool Public { get; set; }
        public int Version { get; set; }

        public string LocationName { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }

        public LocationModel Location { get; set; }
        public string ImageId { get; set; }

        public string ProfileImageId { get; set; }
        public string ProfileName { get; set; }

        public DateTime CreationTime { get; set; }

        public int Votes { get; set; }
        public int Plays { get; set; }
        public int Finishes { get; set; }

        public QuestModel(string id, ObjectId ownerId, string ownerName, string? ownerImageId, bool @public, int version, string locationName, string title, string description, List<string> tags, LocationModel location, string imageId, string profileImageId, string profileName, DateTime creationTime, int votes, int plays, int finishes)
        {
            Id = id;
            OwnerId = ownerId;
            OwnerName = ownerName;
            OwnerImageId = ownerImageId;
            Public = @public;
            Version = version;
            LocationName = locationName;
            Title = title;
            Description = description;
            Tags = tags;
            Location = location;
            ImageId = imageId;
            ProfileImageId = profileImageId;
            ProfileName = profileName;
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
            ProfileImageId = default!;
            ProfileName = default!;
            CreationTime = default!;
            Votes = default!;
            Plays = default!;
            Finishes = default!;
        }
    }
}
