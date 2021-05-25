using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.ModuleAggregate.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.QuestPrototypeAggregate.Modules
{
    public class ChoiceModulePrototype : ModulePrototype
    {
        public List<Choice> Choices { get; }

        private ChoiceModulePrototype()
        {
            Choices = default!;
        }

        public override Module ConvertToModule(IPrototypeContext context)
            => new ChoiceModule(
                context.ConvertModuleId(Id),
                Objective,
                Choices.Select(c => new ChoiceModule.Choice(context.ConvertModuleId(c.ModuleId), c.Text))
                       .ToList());

        public class Choice
        {
            public string Text { get; }
            public int ModuleId { get; }

            public Choice(string text, int moduleId)
            {
                Text = text;
                ModuleId = moduleId;
            }
        }
    }
}
