using Emerald.Domain.Models.TrackerAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emerald.Domain.Models.ModuleAggregate
{
    public class NextModuleResponseEvent : ResponseEvent
    {
        public ObjectId ModuleId { get; }

        public NextModuleResponseEvent(TrackerPathMemento memento, ObjectId moduleId) : base(memento)
        {
            this.ModuleId = moduleId;
        }
    }
}
