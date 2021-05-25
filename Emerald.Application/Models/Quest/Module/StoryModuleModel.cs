using Emerald.Application.Models.Quest.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
