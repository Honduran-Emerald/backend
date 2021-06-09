﻿using Emerald.Application.Models.Quest.Tracker;
using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Domain.Models.TrackerAggregate;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Domain.Repositories;
using Emerald.Infrastructure.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public class TrackerModelFactory : IModelFactory<Tracker, TrackerModel>
    {
        private IQuestRepository questRepository;
        private IModuleRepository moduleRepository;
        private IUserRepository userRepository;

        public TrackerModelFactory(IQuestRepository questRepository, IModuleRepository moduleRepository, IUserRepository userRepository)
        {
            this.questRepository = questRepository;
            this.moduleRepository = moduleRepository;
            this.userRepository = userRepository;
        }

        public async Task<TrackerModel> Create(Tracker source, Quest quest)
        {
            QuestVersion questVersion = quest.GetQuestVersion(source.QuestVersion);
            QuestVersion? stableQuestVersion = quest.GetCurrentQuestVersion();
            Module module = await moduleRepository.Get(source.Nodes.Last().ModuleId);
            User owner = await userRepository.Get(quest.OwnerUserId);

            if (stableQuestVersion == null)
            {
                throw new Exception("Tracker can not exists without stable quest version");
            }

            return new TrackerModel(
                source.QuestId,
                source.Id,
                newestQuestVersion: questVersion.Version == stableQuestVersion.Version,
                finished: source.Finished,
                vote: source.Vote,
                creationTime: source.CreatedAt,
                questVersion.Title,
                module.Objective,
                owner.UserName);
        }

        public async Task<TrackerModel> Create(Tracker source)
        {
            return await Create(source, await questRepository.Get(source.QuestId));
        }
    }
}
