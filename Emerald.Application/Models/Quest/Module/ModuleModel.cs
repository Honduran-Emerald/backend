using Emerald.Application.Models.Quest.Component;
using System.Collections.Generic;

namespace Emerald.Application.Models.Quest.Module
{
    public abstract class ModuleModel
    {
        public string Id { get; set; }

        public string Objective { get; set; }
        public ModuleType Type { get; set; }
        public List<ComponentModel> Components { get; set; }

        public ModuleModel(string moduleId, string objective, ModuleType type, List<ComponentModel> components)
        {
            Id = moduleId;
            Objective = objective;
            Type = type;
            Components = components;
        }

        public ModuleModel()
        {
            Id = default!;
            Type = default!;
            Objective = default!;
            Components = default!;
        }
    }
}
