using Emerald.Domain.Models.TrackerAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emerald.Domain.Models.ModuleAggregate.RequestEvents
{
    public class ChoiceEvent : RequestEvent
    {
        public int Choice { get; }

        public ChoiceEvent(TrackerPathMemento memento, int choice) : base(memento)
        {
            Choice = choice;
        }
    }
}
