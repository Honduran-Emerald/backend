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
    public class QrCodeModulePrototype : ModulePrototype
    {
        public int? NextModuleReference { get; set; }
        public string? Text { get; set; }

        public override Module ConvertToModule(IPrototypeContext context)
            => new QrCodeModule(context.ConvertModuleId(Id), Objective, Text!, context.ConvertModuleId(NextModuleReference!.Value));

        public override void Verify()
        {
            if (NextModuleReference == null)
            {
                throw new DomainException($"({Id}) NextModuleId can not be null");
            }

            if (object.ReferenceEquals(Text, null))
            {
                throw new DomainException($"({Id}) Text can not be null");
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
