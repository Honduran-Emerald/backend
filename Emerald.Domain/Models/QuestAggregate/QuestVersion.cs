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
        public int Version { get; private set; }
        public bool Public { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public List<string> Tags { get; private set; }

        public Location Location { get; private set; }
        public string ImageId { get; private set; }
        public string ApproximateTime { get; private set; }

        public string ProfileImageId { get; private set; }
        public string ProfileName { get; private set; }

        public DateTime CreationTime { get; private set; }

        public List<ObjectId> ModuleIds { get; private set; }
        public ObjectId FirstModuleId { get; private set; }

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

            ModuleIds = default!;
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
