using Emerald.Application.Models.Quest.Component;
using System.Collections.Generic;

namespace Emerald.Application.Models.Quest.Module
{
    public class ModuleModel
    {
        public ModuleType Type { get; set; }
        public List<ComponentModel> Components { get; set; }

        public ModuleModel(ModuleType type, List<ComponentModel> components)
        {
            Type = type;
            Components = components;
        }
    }
}
