﻿using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.ModuleAggregate.Modules;
using Emerald.Domain.Models.QuestPrototypeAggregate.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Choices.Select(c => new ChoiceModule.Choice(context.ConvertModuleId(c.NextModuleId), c.Text))
                       .ToList());

        public override void Verify(IPrototypeContext context)
        {
            if (Choices.Count == 0)
            {
                throw new DomainException($"({Id}) Choicemodule missing any choices");
            }

            foreach (ChoiceModulePrototypeChoice choice in Choices)
            {
                if (context.ContainsModuleId(choice.NextModuleId) == false)
                    throw new DomainException($"({Id}) Choice NextModuleId in ChoiceModule not found got {choice.NextModuleId}");
            }
        }
    }
    public class ChoiceModulePrototypeChoice
    {
        public string Text { get; set; }
        public int NextModuleId { get; set; }

        public ChoiceModulePrototypeChoice(string text, int moduleId)
        {
            Text = text;
            NextModuleId = moduleId;
        }

        private ChoiceModulePrototypeChoice()
        {
            Text = default!;
            NextModuleId = default!;
        }
    }
}
