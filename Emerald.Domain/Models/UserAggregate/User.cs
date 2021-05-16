using AspNetCore.Identity.Mongo.Model;
using Emerald.Domain.Models.TrackerAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.UserAggregate
{
    public class User : MongoUser
    {
        public List<ObjectId> QuestIds { get; private set; }
        public List<ObjectId> TrackerIds { get; private set; }
        public ObjectId? ActiveTrackerId { get; private set; }

        public User() : base()
        {
            QuestIds = new List<ObjectId>();
            TrackerIds = new List<ObjectId>();
        }

        public User(string userName) : base(userName)
        {
            QuestIds = new List<ObjectId>();
            TrackerIds = new List<ObjectId>();
        }

        public void SetActiveTracker(Tracker tracker)
        {
            if (TrackerIds.Contains(tracker.Id) == false)
            {
                throw new DomainException("Can not set missing tracker as active");
            }

            ActiveTrackerId = tracker.Id;
        }
    }
}
