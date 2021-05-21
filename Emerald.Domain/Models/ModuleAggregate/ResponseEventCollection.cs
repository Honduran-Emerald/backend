using Emerald.Domain.Models.TrackerAggregate;
using MediatR;
using System.Collections.Generic;

namespace Emerald.Domain.Models.ModuleAggregate
{
    public class ResponseEventCollection
    {
        public TrackerNodeMemento Memento { get; private set; }
        public List<IResponseEvent> Events { get; private set; }

        public ResponseEventCollection(TrackerNodeMemento memento, List<IResponseEvent> events)
        {
            Memento = memento;
            Events = events;
        }
    }
}
