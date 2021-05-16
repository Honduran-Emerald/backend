using Emerald.Application.Models.Quest.Module;

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
