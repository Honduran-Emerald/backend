using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.ModuleAggregate.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestPrototypeAggregate.Modules
{
    public class RandomModulePrototype : ModulePrototype
    {
        public int? NextLeftModuleReference { get; set; }
        public int? NextRightModuleReference { get; set; }
        public float? LeftRatio { get; set; }

        public override Module ConvertToModule(IPrototypeContext context)
            => new RandomModule(context.ConvertModuleId(Id), Objective, context.ConvertModuleId(NextLeftModuleReference!.Value), context.ConvertModuleId(NextRightModuleReference!.Value), LeftRatio!.Value);

        public override void Verify()
        {
            if (NextLeftModuleReference == null)
            {
                throw new DomainException($"({Id}) NextLeftModuleReference can not be null");
            }
            if (NextRightModuleReference == null)
            {
                throw new DomainException($"({Id}) NextRightModuleReference can not be null");
            }

            if (LeftRatio == null)
            {
                throw new DomainException($"({Id}) LeftRatio can not be null");
            }
        }

        public override void AggregateImageReferences(List<int> imageReferences)
        {
        }

        public override void AggregateModuleReferences(List<int> moduleReferences)
        {
            if (NextLeftModuleReference != null)
            {
                moduleReferences.Add(NextLeftModuleReference.Value);
            }

            if (NextRightModuleReference != null)
            {
                moduleReferences.Add(NextRightModuleReference.Value);
            }
        }
    }
}
