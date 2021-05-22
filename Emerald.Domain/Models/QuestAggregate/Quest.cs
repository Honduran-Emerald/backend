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
        public ObjectId OwnerUserId { get; protected set; }
        public List<QuestVersion> QuestVersions { get; private set; }

        public Quest(User user, Location location, string title, string description, string image)
            : this()
        {
            OwnerUserId = user.Id;
            QuestVersions.Add(new QuestVersion(
                location, title, description, image, 1));
        }

        private Quest()
        {
            QuestVersions = new List<QuestVersion>();
        }

        public QuestVersion GetQuestVersion(long version)
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

        public QuestVersion? GetStableQuestVersion()
        {
            return QuestVersions
                .Where(q => q.Published)
                .OrderByDescending(q => q.Version)
                .FirstOrDefault();
        }

        public QuestVersion GetDevelopmentQuestVersion()
        {
            return QuestVersions
                .OrderByDescending(q => q.Version)
                .First();
        }

        public void RemoveQuestVersion(QuestVersion questVersion)
        {
            QuestVersions.Remove(questVersion);
        }

        public QuestVersion PublishQuestVersion()
        {
            QuestVersion questVersion = GetDevelopmentQuestVersion();
            questVersion.Publish();

            QuestVersion newQuestVersion = new QuestVersion(
                questVersion.Location,
                questVersion.Title,
                questVersion.Description,
                questVersion.Image,
                questVersion.Version + 1);

            QuestVersions.Add(newQuestVersion);
            return newQuestVersion;
        }
    }
}
