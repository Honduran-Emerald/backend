using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.ModuleAggregate.Modules;
using System.Collections.Generic;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestPrototypeAggregate.Modules
{
    public class EndingModulePrototype : ModulePrototype
    {
        public float EndingFactor { get; set; }

        public EndingModulePrototype()
        {
            EndingFactor = default!;
        }

        public override Module ConvertToModule(IPrototypeContext context)
            => new EndingModule(context.ConvertModuleId(Id), Objective, EndingFactor);

        public override void Verify()
        {
            if (EndingFactor < 0 || EndingFactor > 1)
            {
                throw new DomainException($"({Id}) Endingfactor in EndingModule has to be between 0 and 1 got {EndingFactor}");
            }
        }

        public override void AggregateImageReferences(List<int> imageReferences)
        {
        }

        public override void AggregateModuleReferences(List<int> moduleReferences)
        {
        }
    }
}
