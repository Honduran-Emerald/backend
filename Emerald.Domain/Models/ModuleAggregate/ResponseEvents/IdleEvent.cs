﻿using Emerald.Domain.Models.TrackerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.ModuleAggregate.ResponseEvents
{
    public class IdleEvent : ResponseEvent
    {
        public IdleEvent(TrackerPathMemento memento) : base(memento)
        {
        }
    }
}
