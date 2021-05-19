using Emerald.Application.Models.Quest.Component;
using System.Collections.Generic;

namespace Emerald.Application.Models.Quest.Module
{
    public class ChoiceModuleModel : ModuleModel
    {
        public List<Choice> Choices { get; set; }

        public ChoiceModuleModel(
            string id,
            string objective,
            List<Choice> choices, 
            List<ComponentModel> components)
            : base(id, ModuleType.Choice, objective, components)
        {
            Choices = choices;
        }

        public class Choice
        {
            public string Text { get; set; }
            public string ModuleId { get; set; }
        }
    }
}
