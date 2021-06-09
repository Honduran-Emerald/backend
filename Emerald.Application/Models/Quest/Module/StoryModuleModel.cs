using Emerald.Application.Models.Quest.Component;
using System.Collections.Generic;

namespace Emerald.Application.Models.Quest.Module
{
    public class StoryModuleModel : ModuleModel
    {
        public StoryModuleModel()
        {
        }

        public StoryModuleModel(string moduleId, string objective, List<ComponentModel> components)
            : base(moduleId, objective, ModuleType.Story, components)
        {
        }
    }
}
