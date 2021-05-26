﻿using Emerald.Domain.Models.ComponentAggregate;
using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.QuestPrototypeAggregate.Modules;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.QuestPrototypeAggregate
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(
        typeof(ChoiceModulePrototype),
        typeof(EndingModulePrototype),
        typeof(StoryModulePrototype))]
    public abstract class ModulePrototype
    {
        public int Id { get; set; }

        public List<ComponentPrototype> Components { get; set; }
        public string Objective { get; set; }

        public ModulePrototype(int id, List<ComponentPrototype> components, string objective)
        {
            Id = id;
            Components = components;
            Objective = objective;
        }

        public ModulePrototype()
        {
            Id = default!;
            Components = default!;
            Objective = default!;
        }

        public abstract Module ConvertToModule(IPrototypeContext context);

        public List<Component> ConvertToComponents()
            => Components.Select(c => c.ConvertToComponent())
                         .ToList();
    }
}
