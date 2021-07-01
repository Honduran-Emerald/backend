using Emerald.Application.Models.Quest.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.Module
{
    public class LocationModuleModel : ModuleModel
    {
        public LocationModel Location { get; set; }

        public LocationModuleModel(string moduleId, string objective, List<ComponentModel> components, LocationModel locationModel)
               : base(moduleId, objective, ModuleType.Location, components)
        {
            Location = locationModel;
        }
    }
}
