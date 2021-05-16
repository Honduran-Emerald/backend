using Emerald.Domain.Models.TrackerAggregate;

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
