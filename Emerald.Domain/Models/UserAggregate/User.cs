using AspNetCore.Identity.Mongo.Model;
using Emerald.Domain.Models.LockAggregate;
using Emerald.Domain.Models.TrackerAggregate;
using Emerald.Domain.SeedWork;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Vitamin.Value.Domain;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.UserAggregate
{
    public class User : MongoUser, IEntity<ObjectId>
    {
        public List<ObjectId> QuestIds { get; set; }
        public List<ObjectId> TrackerIds { get; set; }

        public List<Lock> Locks { get; set; }

        public List<ObjectId> Following { get; set; }
        public List<ObjectId> Followers { get; set; }

        public string? ImageId { get; set; }
        public string SyncToken { get; set; }
        public string? MessagingToken { get; set; }

        public long Experience { get; set; }
        public int Glory { get; set; }

        private List<INotification> domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => domainEvents;

        public User() : base()
        {
            Locks = new List<Lock>();
            QuestIds = new List<ObjectId>();
            TrackerIds = new List<ObjectId>();
            domainEvents = new List<INotification>();
            SyncToken = Utility.RandomString(8);
            Followers = new List<ObjectId>();
            Following = new List<ObjectId>();
        }

        public User(string userName) : base(userName)
        {
            Locks = new List<Lock>();
            QuestIds = new List<ObjectId>();
            TrackerIds = new List<ObjectId>();
            domainEvents = new List<INotification>();
            SyncToken = Utility.RandomString(8);
            Followers = new List<ObjectId>();
            Following = new List<ObjectId>();
        }

        public void AddExperience(long experience)
        {
            Experience += (long) (experience * (2 + new Random().NextDouble()));
        }

        public void AddTracker(Tracker tracker)
        {
            if (TrackerIds.Contains(tracker.Id))
            {
                throw new DomainException("Tracker already added to user");
            }

            if (tracker.UserId != Id)
            {
                throw new DomainException("Tracker already added to other user");
            }

            TrackerIds.Add(tracker.Id);
        }

        public void GenerateNewSyncToken()
        {
            SyncToken = Utility.RandomString(8);
        }

        public int GetLevel()
        {
            return (int)Math.Floor(Math.Sqrt((Experience + 22562.5) / 250.0) - 8.5);
        }

        public void ClearEvents()
        {
            domainEvents.Clear();
        }

        protected void AddDomainEvent(INotification domainEvent)
        {
            if (domainEvents == null)
                domainEvents = new List<INotification>();

            domainEvents.Add(domainEvent);
        }

        public void Follow(User user)
        {
            if (Following.Contains(user.Id))
            {
                throw new DomainException($"Already following {user.UserName}");
            }

            Following.Add(user.Id);
            user.Followers.Add(Id);
        }

        public void Unfollow(User user)
        {
            if (Following.Contains(user.Id) == false)
            {
                throw new DomainException($"Not following {user.UserName}");
            }

            Following.Remove(user.Id);
            user.Followers.Remove(Id);
        }
    }
}
