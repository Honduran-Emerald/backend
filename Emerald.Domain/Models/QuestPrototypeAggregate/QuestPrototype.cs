using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.ModuleAggregate.Modules;
using Emerald.Domain.Models.QuestPrototypeAggregate;
using Emerald.Domain.Models.QuestPrototypeAggregate.Modules;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public string? ImageId { get; set; }
        public string? ApproximateTime { get; set; }

        public string? ProfileImageId { get; set; }
        public string? ProfileName { get; set; }

        public int? FirstModuleId { get; set; }
        public List<ModulePrototype> Modules { get; set; }

        public QuestPrototype(string title, string description, List<string> tags, string locationName, Location location, string imageId)
        {
            Title = title;
            Description = description;
            Tags = tags;
            LocationName = locationName;
            Location = location;
            ImageId = imageId;
            Modules = new List<ModulePrototype>();
        }

        public QuestPrototype()
        {
            Tags = new List<string>();
            Modules = new List<ModulePrototype>();
        }

        public void Verify(IPrototypeContext context)
        {
            if (Title == null)
            {
                throw new DomainException("Quest Title can not be null");
            }

            if (Description == null)
            {
                throw new DomainException("Quest Description can not be null");
            }

            if (LocationName == null)
            {
                throw new DomainException("Quest LocationName can not be null");
            }

            if (ReferenceEquals(Location, null))
            {
                throw new DomainException("Quest Location can not be null");
            }

            if (ImageId == null)
            {
                throw new DomainException("Quest ImageId can not be null");
            }

            if (ApproximateTime == null)
            {
                throw new DomainException("ApproximateTime can not be null");
            }

            if (ApproximateTime == null)
            {
                throw new DomainException("ApproximateTime can not be null");
            }

            if (ProfileImageId == null)
            {
                throw new DomainException($"Quest ProfileImageId can not be null");
            }

            if (ProfileName == null)
            {
                throw new DomainException($"Quest ProfileName can not be null");
            }

            if (FirstModuleId == null)
            {
                throw new DomainException($"Quest FirstModuleId can not be null");
            }

            if (context.ContainsModuleId(FirstModuleId.Value) == false)
            {
                throw new DomainException("FirstModuleId is not found in modules");
            }

            foreach (ModulePrototype module in Modules)
            {
                module.Verify(context);

                foreach (ComponentPrototype component in module.Components)
                    component.Verify();
            }
        }
    }
}
