using MongoDB.Bson;
using System;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.TrackerAggregate
{
    public class TrackerNode : Entity
    {
        public ObjectId ModuleId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public TrackerNodeMemento Memento { get; private set; }

        public TrackerNode(ObjectId moduleId)
        {
            ModuleId = moduleId;

            CreatedAt = DateTime.UtcNow;
            Memento = null;
        }

        private TrackerNode()
        {
        }

        public void UpdateMemento(TrackerNodeMemento memento)
        {
            Memento = memento;
        }
    }
}
