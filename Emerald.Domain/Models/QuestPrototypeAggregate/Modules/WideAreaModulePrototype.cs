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
    public class WideAreaModulePrototype : ModulePrototype
    {
        public int? NextModuleReference { get; set; }
        public Location? Location { get; set; }
        public float Radius { get; set; }

        public override Module ConvertToModule(IPrototypeContext context)
            => new WideAreaModule(context.ConvertModuleId(Id), Objective, Location!, Radius, context.ConvertModuleId(NextModuleReference!.Value));

        public override void Verify()
        {
            if (NextModuleReference == null)
            {
                throw new DomainException($"({Id}) NextModuleId can not be null");
            }

            if (object.ReferenceEquals(Location, null))
            {
                throw new DomainException($"({Id}) Location can not be null");
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
