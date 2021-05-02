using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Domain.Models.UserAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestAggregate
{
    public class Quest : Entity
    {
        public override ObjectId Id { get; protected set; }
        public ObjectId OwnerUserId { get; protected set; }

        public List<ObjectId> QuestVersions { get; private set; }
        public ObjectId StableQuestVersion { get; private set; }
        
        public Quest(User user)
            : this()
        {
            OwnerUserId = user.Id;
        }

        private Quest()
        {
            QuestVersions = new List<ObjectId>();
        }

        public void SetStableQuestVersion(QuestVersion questVersion)
        {
            if (QuestVersions.Contains(questVersion.Id) == false)
            {
                throw new DomainException("Stable quest version has to be already a quest version");
            }
        }

        public void AddQuestVersion(QuestVersion questVersion)
        {
            if (QuestVersions.Contains(questVersion.Id))
            {
                throw new DomainException("Can not add already existing questversion");
            }

            if (questVersion.QuestId != Id)
            {
                throw new DomainException("Can not add questversion from other quest");
            }

            QuestVersions.Add(questVersion.Id);
        }
    }
}
