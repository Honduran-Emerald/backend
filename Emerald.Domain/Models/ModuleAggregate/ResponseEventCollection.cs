using Emerald.Domain.Models.TrackerAggregate;
using MediatR;
using System.Collections.Generic;

namespace Emerald.Domain.Models.ModuleAggregate
{
    public class ResponseEventCollection
    {
        public TrackerNodeMemento? Memento { get; set; }
        public List<IResponseEvent> Events { get; set; }

        public ResponseEventCollection(TrackerNodeMemento? memento, List<IResponseEvent> events)
        {
            Memento = memento;
            Events = events;
        }
    }
}
