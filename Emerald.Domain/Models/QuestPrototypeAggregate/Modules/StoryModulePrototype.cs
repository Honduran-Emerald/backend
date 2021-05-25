using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.ModuleAggregate.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.QuestPrototypeAggregate.Modules
{
    public class StoryModulePrototype : ModulePrototype
    {
        public int NextModuleId { get; }

        private StoryModulePrototype(int nextModuleId)
        {
            NextModuleId = default!;
        }

        public override Module ConvertToModule(IPrototypeContext context)
            => new StoryModule(context.ConvertModuleId(NextModuleId));
    }
}
