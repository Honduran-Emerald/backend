using Emerald.Domain.Models.TrackerAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emerald.Domain.Models.ModuleAggregate.RequestEvents
{
    public class ChoiceRequestEvent : RequestEvent
    {
        public int Choice { get; }

        public ChoiceRequestEvent(int choice)
        {
            Choice = choice;
        }
    }
}
