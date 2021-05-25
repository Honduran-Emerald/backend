using Emerald.Application.Models.Quest.Component;
using System.Collections.Generic;
using System.Linq;

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
            : base(id, objective, ModuleType.Choice, 
                  choices
                    .Select(c => c.ModuleId)
                    .ToList(), 
                  components)
        {
            Choices = choices;
        }

        private ChoiceModuleModel()
        {
            Choices = default!;
        }

        public class Choice
        {
            public string Text { get; }
            public long ModuleId { get; }

            public Choice(string text, long moduleId)
            {
                Text = text;
                ModuleId = moduleId;
            }
        }
    }
}
