using Emerald.Application.Models.Quest.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.Module
{
    public class ChoiceModuleModel : ModuleModel
    {
        public List<string> Choices { get; set; }

        public ChoiceModuleModel(List<string> choices, List<ComponentModel> components) 
            : base(ModuleType.Choice, components)
        {
            Choices = choices;
        }
    }
}
