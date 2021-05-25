using Emerald.Application.Models.Quest.Tracker;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Domain.Models.TrackerAggregate;
using Emerald.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public class TrackerModelFactory : IModelFactory<Tracker, TrackerModel>
    {
        private IQuestRepository questRepository;

        public TrackerModelFactory(IQuestRepository questRepository)
        {
            this.questRepository = questRepository;
        }

        public async Task<TrackerModel> Create(Tracker source)
        {
            Quest quest = await questRepository.Get(source.QuestId);
            QuestVersion questVersion = quest.GetQuestVersion(source.QuestVersion);
            QuestVersion? stableQuestVersion = quest.GetCurrentQuestVersion();

            if (stableQuestVersion == null)
            {
                throw new Exception("Tracker can not exists without stable quest version");
            }

            return new TrackerModel(
                newestQuestVersion: questVersion.Version == stableQuestVersion.Version,
                finished: source.Finished,
                vote: source.Vote,
                creationTime: source.CreatedAt);
        }
    }
}
