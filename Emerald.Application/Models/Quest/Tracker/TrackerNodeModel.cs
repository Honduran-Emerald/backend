using Emerald.Application.Models.Quest.Module;
using Emerald.Application.Models.Quest.ModuleMemento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.Tracker
{
    public class TrackerNodeModel
    {
        public MementoModel Memento { get; set; }
        public ModuleModel Module { get; set; }
        public DateTime CreationTime { get; set; }

        public TrackerNodeModel(MementoModel memento, ModuleModel module, DateTime creationTime)
        {
            Memento = memento;
            Module = module;
            CreationTime = creationTime;
        }
    }
}
