using Emerald.Application.Models.Quest.Component;
using System.Collections.Generic;

namespace Emerald.Application.Models.Quest.Module
{
    public class ChoiceModuleModel : ModuleModel
    {
        public List<ChoiceModuleModelChoice> Choices { get; set; }

        public ChoiceModuleModel(string id, string objective, List<ChoiceModuleModelChoice> choices, List<ComponentModel> components)
            : base(id, objective, ModuleType.Choice, components)
        {
            Choices = choices;
        }

        private ChoiceModuleModel()
        {
            Choices = default!;
        }

        public class ChoiceModuleModelChoice
        {
            public string Text { get; }

            public ChoiceModuleModelChoice(string text)
            {
                Text = text;
            }
        }
    }
}
