using Emerald.Domain.Models.QuestVersionAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.TrackerAggregate
{
    public class TrackerPath : Entity
    {
        public override ObjectId Id { get; protected set; }

        public ObjectId ModuleId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public TrackerPathMemento Memento { get; private set; }

        public TrackerPath(ObjectId moduleId)
        {
            ModuleId = moduleId;

            CreatedAt = DateTime.UtcNow;
            Memento = null;
        }

        private TrackerPath()
        {
        }

        public void UpdateMemento(TrackerPathMemento memento)
        {
            Memento = memento;
        }
    }
}
