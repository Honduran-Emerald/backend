using Emerald.Domain.Models.TrackerAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emerald.Domain.Models.ModuleAggregate
{
    public class RequestEvent
    {
        public TrackerPathMemento Memento { get; }

        public RequestEvent(TrackerPathMemento memento)
        {
            Memento = memento;
        }

        public RequestEvent()
        {
        }
    }
}
