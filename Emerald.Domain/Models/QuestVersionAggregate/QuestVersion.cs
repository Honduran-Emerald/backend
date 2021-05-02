using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestVersionAggregate
{
    public class QuestVersion : Entity
    {
        public override ObjectId Id { get; protected set; }

        public bool Published { get; private set; }

        public ObjectId QuestId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public long Version { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public List<Module> Modules { get; private set; }
        public Module FirstModule { get; private set; }

        public QuestVersion(ObjectId questId, string title, string description, long version)
        {
            QuestId = questId;
            Title = title;
            Description = description;
            Version = version;

            CreatedAt = DateTime.UtcNow;
        }

        private QuestVersion()
        {
        }

        public void ChangeFirstModule(Module module)
        {
            if (Modules.Contains(module) == false)
            {
                throw new DomainException("First module has to be already in all modules");
            }

            FirstModule = module;
        }

        public void AddModule(Module module)
        {
            if (Modules.Contains(module))
            {
                throw new DomainException("Can not add already existing module");
            }

            if (module.QuestVersion.Id != Id)
            {
                throw new DomainException("Can not add module from other questversion");
            }

            Modules.Add(module);
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
