using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.ModuleAggregate.Modules;
using System.Collections.Generic;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestPrototypeAggregate.Modules
{
    public class StoryModulePrototype : ModulePrototype
    {
        public int? NextModuleReference { get; set; }

        private StoryModulePrototype()
        {
            NextModuleReference = default!;
        }

        public override Module ConvertToModule(IPrototypeContext context)
            => new StoryModule(context.ConvertModuleId(Id), Objective, context.ConvertModuleId(NextModuleReference!.Value));

        public override void Verify()
        {
            if (NextModuleReference == null)
            {
                throw new DomainException($"({Id}) NextModuleId can not be null");
            }
        }

        public override void AggregateImageReferences(List<int> imageReferences)
        {
        }

        public override void AggregateModuleReferences(List<int> moduleReferences)
        {
            if (NextModuleReference != null)
            {
                moduleReferences.Add(NextModuleReference.Value);
            }
        }
    }
}
