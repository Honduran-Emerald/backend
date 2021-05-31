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

#nullable disable

namespace Emerald.Domain.Models.PrototypeAggregate
{
    public class QuestPrototype : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }

        public string LocationName { get; set; }
        public Location Location { get; set; }
        public string ImageId { get; set; }
        public string ApproximateTime { get; set; }

        public string ProfileImageId { get; set; }
        public string ProfileName { get; set; }

        public int FirstModuleId { get; set; }
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
        }

        public void Verify(IPrototypeContext context)
        {
            if (context.ContainsModuleId(FirstModuleId) == false)
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
