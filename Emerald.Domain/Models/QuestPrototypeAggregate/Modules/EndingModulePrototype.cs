using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.ModuleAggregate.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.QuestPrototypeAggregate.Modules
{
    public class EndingModulePrototype : ModulePrototype
    {
        public float EndingFactor { get; }

        public EndingModulePrototype()
        {
            EndingFactor = default!;
        }

        public override Module ConvertToModule(IPrototypeContext context)
            => new EndingModule(context.ConvertModuleId(Id), Objective, EndingFactor);
    }
}
