using Emerald.Application.Models.Quest.Component;
using System.Collections.Generic;

namespace Emerald.Application.Models.Quest.Module
{
    public class EndingModuleModel : ModuleModel
    {
        public float EndingFactor { get; set; }

        private EndingModuleModel()
        {
        }

        public EndingModuleModel(string moduleId, string objective, List<ComponentModel> components, float endingFactor) : base(moduleId, objective, ModuleType.Ending, components)
        {
            EndingFactor = endingFactor;
        }
    }
}
