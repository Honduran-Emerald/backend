using Emerald.Domain.Models.PrototypeAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Domain.Models.UserAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestAggregate
{
    public class Quest : Entity
    {
        public ObjectId OwnerUserId { get; set; }
        public ObjectId PrototypeId { get; set; }

        public bool Public { get; set; }
        public List<QuestVersion> QuestVersions { get; set; }
        public DateTime CreationTime { get; set; }

        public Quest(User user, QuestPrototype questPrototype)
        {
            Public = true;
            QuestVersions = new List<QuestVersion>();
            OwnerUserId = user.Id;
            PrototypeId = questPrototype.Id;
            CreationTime = DateTime.UtcNow;
        }

        private Quest()
        {
            Public = true;
            QuestVersions = new List<QuestVersion>();
            CreationTime = DateTime.UtcNow;
        }

        public int GetCurrentQuestVersionNumber()
            => QuestVersions.Count == 0 ? 1 : QuestVersions.Max(qv => qv.Version);

        public QuestVersion GetQuestVersion(int version)
        {
            QuestVersion? questVersion = QuestVersions
                .Where(q => q.Version == version)
                .FirstOrDefault();

            if (questVersion == null)
            {
                throw new DomainException("Questversion not found by version");
            }

            return questVersion;
        }

        public bool CanAccess(User user)
        {
            return Public || user.Id == OwnerUserId;
        }

        public QuestVersion? GetCurrentQuestVersion()
        {
            return QuestVersions
                .OrderByDescending(q => q.Version)
                .FirstOrDefault();
        }

        public void RemoveQuestVersion(QuestVersion questVersion)
        {
            QuestVersions.Remove(questVersion);
        }

        public QuestVersion PublishQuestVersion(QuestPrototype questPrototype, List<ObjectId> moduleIds, ObjectId firstModuleId)
        {
            QuestVersion newQuestVersion = new QuestVersion(
                questPrototype,
                GetCurrentQuestVersionNumber() + 1,
                moduleIds,
                firstModuleId);

            QuestVersions.Add(newQuestVersion);
            return newQuestVersion;
        }
    }
}
