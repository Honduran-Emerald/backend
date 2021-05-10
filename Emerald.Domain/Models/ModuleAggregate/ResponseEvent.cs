using Emerald.Domain.Models.TrackerAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emerald.Domain.Models.ModuleAggregate
{
    public class ResponseEvent
    {
        public TrackerPathMemento Memento { get; private set; }

        public ResponseEvent(TrackerPathMemento memento)
        {
            Memento = memento;
        }
    }
}
