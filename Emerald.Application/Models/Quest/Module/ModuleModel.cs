using Emerald.Application.Models.Quest.Component;
using System.Collections.Generic;

namespace Emerald.Application.Models.Quest.Module
{
    public class ModuleModel
    {
        public string ModuleId { get; set; }
        public ModuleType Type { get; set; }
        public List<ComponentModel> Components { get; set; }

        public ModuleModel(string moduleId, ModuleType type, List<ComponentModel> components)
        {
            ModuleId = moduleId;
            Type = type;
            Components = components;
        }
    }
}
