using Emerald.Domain.Models.QuestAggregate;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public QuestVersion(Quest quest, string title, string description, long version)
            : this()
        {
            QuestId = quest.Id;
            Title = title;
            Description = description;
            Version = version;

            CreatedAt = DateTime.UtcNow;
        }

        [BsonConstructor]
        private QuestVersion(List<Module> modules)
        {
            Modules = modules;
            Modules.ForEach(m => m.QuestVersion = this);
        }

        private QuestVersion()
        {
            Modules = new List<Module>();
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
