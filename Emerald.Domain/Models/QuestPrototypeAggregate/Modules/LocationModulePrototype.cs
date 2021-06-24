using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.ModuleAggregate.Modules;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestPrototypeAggregate.Modules
{
    public class LocationModulePrototype : ModulePrototype
    {
        public int? NextModuleReference { get; set; }
        public Location Location { get; set; }

        public LocationModulePrototype(int id, List<ComponentPrototype> components, string objective, int? nextModuleReference, Location location) 
            : base(id, components, objective)
        {
            NextModuleReference = nextModuleReference;
            Location = location;
        }

        public LocationModulePrototype()
        {
            NextModuleReference = default!;
            Location = default!;
        }

        public override void AggregateImageReferences(List<int> imageReferences)
        {
        }

        public override void AggregateModuleReferences(List<int> moduleReferences)
        {
            if (NextModuleReference != null)
                moduleReferences.Add(NextModuleReference.Value);
        }

        public override Module ConvertToModule(IPrototypeContext context)
            => new LocationModule(
                context.ConvertModuleId(Id),
                Objective, 
                context.ConvertModuleId(NextModuleReference!.Value), 
                Location);

        public override void Verify()
        {
            if (NextModuleReference == null)
            {
                throw new DomainException($"({Id}) NextModuleId can not be null");
            }
        }
    }
}
