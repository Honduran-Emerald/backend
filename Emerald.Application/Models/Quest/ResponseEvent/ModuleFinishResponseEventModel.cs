using Emerald.Application.Models.Quest.Module;

namespace Emerald.Application.Models.Quest.Events
{
    public class ModuleFinishResponseEventModel : ResponseEventModel
    {
        public ModuleModel Module { get; set; }

        public ModuleFinishResponseEventModel(ModuleModel module) : base(ResponseEventType.ModuleFinish)
        {
            Module = module;
        }
    }
}
