using Emerald.Application.Models.Quest.Module;
using Emerald.Application.Models.Quest.ModuleMemento;
using System;

namespace Emerald.Application.Models.Quest.Tracker
{
    public class TrackerNodeModel
    {
        public MementoModel? Memento { get; set; }
        public ModuleModel Module { get; set; }
        public DateTime CreationTime { get; set; }
         
        public TrackerNodeModel(MementoModel? memento, ModuleModel module, DateTime creationTime)
        {
            Memento = memento;
            Module = module;
            CreationTime = creationTime;
        }

        public TrackerNodeModel()
        {
            Memento = default!;
            Module = default!;
            CreationTime = default!;
        }
    }
}
