using Emerald.Application.Models.Quest.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.Module
{
    public class WideAreaModuleModel : ModuleModel
    {
        public LocationModel Location { get; set; }
        public float Radius { get; set; }

        public WideAreaModuleModel()
        {
            Location = default!;
        }

        public WideAreaModuleModel(string moduleId, string objective, List<ComponentModel> components, LocationModel locationModel, float radius)
            : base(moduleId, objective, ModuleType.WideArea, components)
        {
            Location = locationModel;
            Radius = radius;
        }
    }
}
