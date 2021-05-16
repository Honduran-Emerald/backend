using Emerald.Application.Models.Quest.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.Events
{
    public class NextModuleResponseEventModel : ResponseEventModel
    {
        public ModuleModel Module { get; set; }

        public NextModuleResponseEventModel(ModuleModel module) : base(ResponseEventType.NextModuleEvent)
        {
            Module = module;
        }
    }
}
