using Emerald.Domain.Models.TrackerAggregate;

namespace Emerald.Domain.Models.ModuleAggregate.ResponseEvents
{
    public class IdleResponseEvent : ResponseEvent
    {
        public IdleResponseEvent(TrackerPathMemento memento) : base(memento)
        {
        }
    }
}
