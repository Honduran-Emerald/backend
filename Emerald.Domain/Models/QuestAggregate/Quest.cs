using Emerald.Domain.Models.PrototypeAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Domain.Models.UserAggregate;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestAggregate
{
    public class Quest : Entity
    {
        public ObjectId OwnerUserId { get; set; }
        public ObjectId PrototypeId { get; set; }
        public List<QuestVersion> QuestVersions { get; set; }

        public Quest(User user, QuestPrototype questPrototype)
        {
            QuestVersions = new List<QuestVersion>();
            OwnerUserId = user.Id;
            PrototypeId = questPrototype.Id;
        }

        private Quest()
        {
            QuestVersions = new List<QuestVersion>();
        }

        public int GetNewestQuestVersion()
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

        public QuestVersion? GetCurrentPrivateQuestVersion()
        {
            return QuestVersions
                .OrderByDescending(q => q.Version)
                .FirstOrDefault();
        }

        public QuestVersion? GetCurrentQuestVersion()
        {
            return QuestVersions
                .Where(q => q.Public)
                .OrderByDescending(q => q.Version)
                .FirstOrDefault();
        }

        public void RemoveQuestVersion(QuestVersion questVersion)
        {
            QuestVersions.Remove(questVersion);
        }

        public QuestVersion PublishQuestVersion(QuestPrototype questPrototype)
        {
            QuestVersion newQuestVersion = new QuestVersion(
                questPrototype,
                GetNewestQuestVersion() + 1);

            QuestVersions.Add(newQuestVersion);
            return newQuestVersion;
        }
    }
}
