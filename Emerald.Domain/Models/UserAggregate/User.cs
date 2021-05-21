using AspNetCore.Identity.Mongo.Model;
using Emerald.Domain.Events;
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
    public class User : MongoUser, IEntity
    {
        public List<ObjectId> QuestIds { get; private set; }
        public List<ObjectId> TrackerIds { get; private set; }
        public ObjectId? ActiveTrackerId { get; private set; }

        public string Image { get; set; }
        public string SyncToken { get; private set; }

        public long Experience { get; private set; }
        public int Glory { get; private set; }

        private List<INotification> domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => domainEvents;

        public User() : base()
        {
            QuestIds = new List<ObjectId>();
            TrackerIds = new List<ObjectId>();
            domainEvents = new List<INotification>();
        }

        public User(string userName) : base(userName)
        {
            QuestIds = new List<ObjectId>();
            TrackerIds = new List<ObjectId>();
            domainEvents = new List<INotification>();
        }

        public void SetActiveTracker(Tracker tracker)
        {
            if (TrackerIds.Contains(tracker.Id) == false)
            {
                throw new DomainException("Can not set missing tracker as active");
            }

            ActiveTrackerId = tracker.Id;
        }

        public void AddExperience(long experience)
        {
            Experience += experience;
        }

        public void GenerateNewSyncToken()
        {
            SyncToken = Utility.RandomString(8);
        }

        public int GetLevel()
        {
            return (int) Math.Round(Math.Sqrt((Experience + 22562.5) / 250.0) - 8.5);
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
    }
}
