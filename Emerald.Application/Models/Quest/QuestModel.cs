﻿using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
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

        public LocationModel Location { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public long Version { get; set; }

        public DateTime CreationTime { get; set; }

        public int Votes { get; set; }
        public int Plays { get; set; }
        public int Finishs { get; set; }
    }
}
