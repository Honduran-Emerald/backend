using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.ModuleAggregate.Modules;
using System.Collections.Generic;
using System.Linq;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestPrototypeAggregate.Modules
{
    public class ChoiceModulePrototype : ModulePrototype
    {
        public List<ChoiceModulePrototypeChoice> Choices { get; set; }

        private ChoiceModulePrototype()
        {
            Choices = new List<ChoiceModulePrototypeChoice>();
        }

        public override Module ConvertToModule(IPrototypeContext context)
            => new ChoiceModule(
                context.ConvertModuleId(Id),
                Objective,
                Choices.Select(c => new ChoiceModule.Choice(context.ConvertModuleId((int)c.NextModuleReference!), c.Text))
                       .ToList());

        public override void Verify()
        {
            if (Choices.Count == 0)
            {
                throw new DomainException($"({Id}) Choicemodule missing any choices");
            }

            foreach (ChoiceModulePrototypeChoice choice in Choices)
            {
                if (choice.NextModuleReference == null)
                    throw new DomainException($"({Id}) NextModuleId in choice can not be null");
            }
        }

        public override void AggregateImageReferences(List<int> imageReferences)
        {
        }

        public override void AggregateModuleReferences(List<int> moduleReferences)
        {
            moduleReferences.AddRange(Choices
                .Where(c => c.NextModuleReference != null)
                .Select(c => c.NextModuleReference!.Value));
        }
    }
    public class ChoiceModulePrototypeChoice
    {
        public string Text { get; set; }
        public int? NextModuleReference { get; set; }

        public ChoiceModulePrototypeChoice(string text, int? moduleId)
        {
            Text = text;
            NextModuleReference = moduleId;
        }

        private ChoiceModulePrototypeChoice()
        {
            Text = default!;
            NextModuleReference = default!;
        }
    }
}
