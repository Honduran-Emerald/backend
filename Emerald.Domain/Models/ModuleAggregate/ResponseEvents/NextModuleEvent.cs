using Emerald.Domain.Models.TrackerAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emerald.Domain.Models.ModuleAggregate
{
    public class NextModuleEvent : ResponseEvent
    {
        public ObjectId ModuleId { get; }

        public NextModuleEvent(TrackerPathMemento memento, ObjectId moduleId) : base(memento)
        {
            this.ModuleId = moduleId;
        }
    }
}
