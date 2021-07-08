using Emerald.Domain.Models.QuestPrototypeAggregate;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.PrototypeAggregate
{
    public class QuestPrototype : Entity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<string> Tags { get; set; }

        public string? LocationName { get; set; }
        public Location? Location { get; set; }
        public int? ImageReference { get; set; }
        public string? ApproximateTime { get; set; }

        public int? AgentProfileReference { get; set; }
        public string? AgentProfileName { get; set; }

        [BsonIgnoreIfNull]
        public bool AgentEnabled { get; set; }

        public int? FirstModuleReference { get; set; }
        public List<ModulePrototype> Modules { get; set; }

        public List<ImagePrototype> Images { get; set; }

        public QuestPrototype()
        {
            Tags = new List<string>();
            Modules = new List<ModulePrototype>();
            Images = new List<ImagePrototype>();
        }

        public void Verify()
        {
            VerifyNotNull();

            VerifyModuleReferences();
            VerifyImageReferences();

            foreach (ModulePrototype module in Modules)
            {
                module.Verify();

                foreach (ComponentPrototype component in module.Components)
                    component.Verify();
            }
        }

        public string? ImageIdByReference(int? reference)
            => Images.FirstOrDefault(i => i.Reference == reference)?.ImageId;

        private void VerifyImageReferences()
        {
            List<int> imageReferences = new List<int>();

            if (ImageReference != null)
                imageReferences.Add(ImageReference.Value);

            if (AgentProfileReference != null)
                imageReferences.Add(AgentProfileReference.Value);

            foreach (ModulePrototype module in Modules)
            {
                module.AggregateImageReferences(imageReferences);

                foreach (ComponentPrototype component in module.Components)
                    component.AggregateImageReferences(imageReferences);
            }

            if (imageReferences.Any(i1 => Images.Any(i2 => i1 == i2.Reference) == false) ||
                Images.Any(i2 => imageReferences.Any(i1 => i1 == i2.Reference) == false))
            {
                throw new DomainException("Got invalid image references");
            }
        }

        private void VerifyModuleReferences()
        {
            List<int> moduleReferences = new List<int>();

            if (FirstModuleReference != null)
                moduleReferences.Add(FirstModuleReference.Value);

            foreach (ModulePrototype module in Modules)
            {
                module.AggregateModuleReferences(moduleReferences);
            }

            List<int> modules = Modules.Select(m => m.Id).ToList();

            if (moduleReferences.Any(m1 => modules.Contains(m1) == false) ||
                modules.Any(m2 => moduleReferences.Contains(m2) == false))
            {
                throw new DomainException("Got invalid module references");
            }
        }

        private void VerifyNotNull()
        {
            if (Title == null)
            {
                throw new DomainException("Quest Title can not be null");
            }

            if (Description == null)
            {
                throw new DomainException("Quest Description can not be null");
            }

            if (ReferenceEquals(Location, null))
            {
                throw new DomainException("Quest Location can not be null");
            }

            if (ApproximateTime == null)
            {
                throw new DomainException("ApproximateTime can not be null");
            }

            if (FirstModuleReference == null)
            {
                throw new DomainException($"Quest FirstModuleReference can not be null");
            }
        }
    }
}
