using Emerald.Domain.Models.PrototypeAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestVersionAggregate
{
    public class QuestVersion
    {
        public int Version { get; set; }

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

        public QuestVersion(QuestPrototype questPrototype, int version, List<ObjectId> moduleIds, ObjectId firstModuleId)
        {
            if (moduleIds.Contains(firstModuleId) == false)
            {
                throw new DomainException("First moduleid missing in modules");
            }

            Version = version;

            Title = questPrototype.Title!;
            Description = questPrototype.Description!;
            Tags = questPrototype.Tags;

            Location = questPrototype.Location!;
            ImageId = questPrototype.ImageIdByReference(questPrototype.ImageReference)!;
            ApproximateTime = questPrototype.ApproximateTime!;

            ProfileImageId = questPrototype.AgentProfileImageId!;
            ProfileName = questPrototype.AgentProfileName!;

            CreationTime = DateTime.UtcNow;

            ModuleIds = new List<ObjectId>();
            ModuleIds.AddRange(moduleIds);
            FirstModuleId = firstModuleId;
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

        private void PlaceModules(List<ObjectId> moduleIds, ObjectId firstModuleId)
        {
            if (moduleIds.Contains(firstModuleId) == false)
            {
                throw new DomainException("First moduleid missing in modules");
            }

            ModuleIds.Clear();
            ModuleIds.AddRange(moduleIds);
            FirstModuleId = firstModuleId;
        }
    }
}
