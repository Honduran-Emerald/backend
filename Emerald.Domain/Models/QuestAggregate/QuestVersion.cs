using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.QuestAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestVersionAggregate
{
    public class QuestVersion : Entity
    {
        public bool Published { get; private set; }

        public Location Location { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }

        public long Version { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public List<ObjectId> ModuleIds { get; private set; }
        public ObjectId FirstModule { get; private set; }

        public QuestVersion(Location location, string title, string description, string image, long version)
        {
            ModuleIds = new List<ObjectId>();

            Location = location;
            Title = title;
            Description = description;
            Image = image;
            Version = version;

            CreatedAt = DateTime.UtcNow;
        }

        private QuestVersion()
        {
            ModuleIds = new List<ObjectId>();
            Location = new Location(0.0, 0.0);
            Title = "Missing Title";
            Description = "Missing Description";
            Image = "";
            Version = 0;
            CreatedAt = DateTime.UtcNow;
        }

        public void ChangeFirstModule(Module module)
        {
            if (ModuleIds.Contains(module.Id) == false)
            {
                throw new DomainException("First module has to be already in all modules");
            }

            FirstModule = module.Id;
        }

        public void AddModule(Module module)
        {
            if (ModuleIds.Contains(module.Id))
            {
                throw new DomainException("Can not add already existing module");
            }

            ModuleIds.Add(module.Id);
        }

        public void RemoveModule(Module module)
        {
            if (ModuleIds.Contains(module.Id) == false)
            {
                throw new DomainException("Can not remove missing module");
            }

            ModuleIds.Remove(module.Id);
        }

        public void Publish()
        {
            if (Published)
            {
                throw new DomainException("Questversion already published");
            }

            Published = true;
        }

        public void ChangeTitle(string title)
        {
            Title = title;
        }

        public void ChangeDescription(string description)
        {
            Description = description;
        }
    }
}
