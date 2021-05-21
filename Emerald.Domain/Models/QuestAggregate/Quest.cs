﻿using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Domain.Models.UserAggregate;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestAggregate
{
    public class Quest : Entity
    {
        public ObjectId Id { get; set; }
        public ObjectId OwnerUserId { get; protected set; }

        public List<QuestVersion> QuestVersions { get; private set; }
        public ObjectId StableQuestVersion { get; private set; }

        public Quest(User user)
            : this()
        {
            OwnerUserId = user.Id;
        }

        private Quest()
        {
            QuestVersions = new List<QuestVersion>();
        }

        public void SetStableQuestVersion(QuestVersion questVersion)
        {
            if (QuestVersions.Any(q => q.Id == questVersion.Id) == false)
            {
                throw new DomainException("Stable quest version has to be already a quest version");
            }

            if (StableQuestVersion == questVersion.Id)
            {
                throw new DomainException("Quest version already set as stable");
            }

            StableQuestVersion = questVersion.Id;
        }

        public QuestVersion GetStableQuestVersion()
        {
            if (QuestVersions.Count == 0)
            {
                throw new DomainException("Quest has no active version");
            }

            return QuestVersions
                .Where(v => v.Id == StableQuestVersion)
                .First();
        }

        public void AddQuestVersion(QuestVersion questVersion)
        {
            if (QuestVersions.Any(q => q.Id == questVersion.Id))
            {
                throw new DomainException("Can not add already existing questversion");
            }

            QuestVersions.Add(questVersion);

            if (QuestVersions.Count == 1)
            {
                SetStableQuestVersion(questVersion);
            }
        }
    }
}
