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

        public string LocationName { get; set; }
        public LocationModel Location { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public long Version { get; set; }

        public DateTime CreationTime { get; set; }

        public int Votes { get; set; }
        public int Plays { get; set; }
        public int Finishs { get; set; }

        public QuestModel(string id, ObjectId ownerId, string locationName, LocationModel location, string title, string description, string image, long version, DateTime creationTime, int votes, int plays, int finishs)
        {
            Id = id;
            OwnerId = ownerId;
            LocationName = locationName;
            Location = location;
            Title = title;
            Description = description;
            Image = image;
            Version = version;
            CreationTime = creationTime;
            Votes = votes;
            Plays = plays;
            Finishs = finishs;
        }

        private QuestModel()
        {
            Id = default!;
            OwnerId = default!;
            LocationName = default!;
            Location = default!;
            Title = default!;
            Description = default!;
            Image = default!;
            Version = default!;
            CreationTime = default!;
            Votes = default!;
            Plays = default!;
            Finishs = default!;
        }
    }
}
