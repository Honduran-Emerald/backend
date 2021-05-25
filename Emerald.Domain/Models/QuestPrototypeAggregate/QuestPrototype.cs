using Emerald.Domain.Models.QuestPrototypeAggregate;
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
        public string Title { get; }
        public string Description { get; }
        public List<string> Tags { get; }

        public Location Location { get; }
        public string ImageId { get; }
        public string ApproximateTime { get; }

        public string ProfileImageId { get; }
        public string ProfileName { get; }

        public int FirstModuleId { get; }
        public List<ModulePrototype> Modules { get; }

        public QuestPrototype(string title, string description, List<string> tags, Location location, string imageId)
        {
            Title = title;
            Description = description;
            Tags = tags;
            Location = location;
            ImageId = imageId;
            ApproximateTime = default!;
            ProfileImageId = default!;
            ProfileName = default!;
            FirstModuleId = default!;
            Modules = default!;
        }

        private QuestPrototype()
        {
            Title = default!;
            Description = default!;
            Tags = default!;
            Location = default!;
            ImageId = default!;
            ApproximateTime = default!;
            ProfileImageId = default!;
            ProfileName = default!;
            FirstModuleId = default!;
            Modules = default!;
        }
    }
}
