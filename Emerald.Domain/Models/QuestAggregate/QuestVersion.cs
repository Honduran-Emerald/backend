using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.PrototypeAggregate;
using Emerald.Domain.Models.QuestAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestVersionAggregate
{
    public class QuestVersion
    {
        public int Version { get; set; }
        public bool Public { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }

        public Location Location { get; set; }
        public string ImageId { get; set; }
        public string ApproximateTime { get; set; }

        public string ProfileImageId { get; set; }
        public string ProfileName { get; set; }

        public DateTime CreationTime { get; set; }

        public List<ObjectId> ModuleIds { get; set; }
        public ObjectId FirstModuleId { get; set; }

        public QuestVersion(QuestPrototype questPrototype, int version)
        {
            Version = version;
            Public = true;

            Title = questPrototype.Title;
            Description = questPrototype.Description;
            Tags = questPrototype.Tags;

            Location = questPrototype.Location;
            ImageId = questPrototype.ImageId;
            ApproximateTime = questPrototype.ApproximateTime;

            ProfileImageId = questPrototype.ProfileImageId;
            ProfileName = questPrototype.ProfileName;

            CreationTime = DateTime.UtcNow;
            ModuleIds = new List<ObjectId>();
        }

        private QuestVersion()
        {
            Version = default!;

            Title = default!;
            Description = default!;
            Tags = default!;

            Location = default!;
            ImageId = default!;
            ApproximateTime = default!;

            ProfileImageId = default!;
            ProfileName = default!;

            ModuleIds = new List<ObjectId>();
        }

        public void PlaceModules(List<ObjectId> moduleIds, ObjectId firstModuleId)
        {
            if (moduleIds.Contains(firstModuleId) == false)
            {
                throw new DomainException("First moduleid missing in modules");
            }

            ModuleIds.Clear();
            ModuleIds.AddRange(moduleIds);
            ModuleIds.Add(firstModuleId);
        }
    }
}
