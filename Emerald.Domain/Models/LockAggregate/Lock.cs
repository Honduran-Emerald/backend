using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.LockAggregate
{
    [BsonKnownTypes(typeof(ImageLock))]
    public abstract class Lock : Entity
    {
        public string Reason { get; set; }

        public List<ObjectId> Quests { get; set; }
        public List<ObjectId> Users { get; set; }
        public List<ObjectId> Trackers { get; set; }

        public Lock(string reason)
        {
            Reason = reason;
            Quests = new List<ObjectId>();
            Users = new List<ObjectId>();
            Trackers = new List<ObjectId>();
        }

        public Lock()
        {
            Reason = default!;
            Quests = new List<ObjectId>();
            Users = new List<ObjectId>();
            Trackers = new List<ObjectId>();
        }
    }
}
