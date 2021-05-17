using Emerald.Application.Models.Quest.Component;
using System.Collections.Generic;

namespace Emerald.Application.Models.Quest.Module
{
    public class ChoiceModuleModel : ModuleModel
    {
        public List<string> Choices { get; set; }

        public ChoiceModuleModel(string id, List<string> choices, List<ComponentModel> components)
            : base(id, ModuleType.Choice, components)
        {
            Choices = choices;
        }
    }
}
