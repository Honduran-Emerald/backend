using Emerald.Domain.Models.ComponentAggregate;
using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Domain.Repositories;
using Emerald.Infrastructure.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Services
{
    public class QuestCreateService
    {
        private IModuleRepository moduleRepository;
        private IComponentRepository componentRepository;
        private IQuestRepository questRepository;
        private ITrackerRepository trackerRepository;

        public QuestCreateService(IModuleRepository moduleRepository, IComponentRepository componentRepository, IQuestRepository questRepository, ITrackerRepository trackerRepository)
        {
            this.moduleRepository = moduleRepository;
            this.componentRepository = componentRepository;
            this.questRepository = questRepository;
            this.trackerRepository = trackerRepository;
        }

        public async Task Publish(Quest quest)
        {
            List<Module> modules = await moduleRepository.GetForQuest(
                quest.GetDevelopmentQuestVersion());

            QuestVersion questVersion = quest.PublishQuestVersion();

            foreach (Module module in modules)
            {
                module.GenerateNewIdentifier();
                List<Component> components = (
                    await componentRepository.GetAll(module.ComponentIds)).ToList();

                module.ClearComponents();
                
                foreach (Component component in components)
                {
                    component.GenerateNewIdentifier();
                    module.AddComponent(component);
                    await componentRepository.Add(component);
                }

                questVersion.AddModule(module);
                await moduleRepository.Add(module);
            }

            await questRepository.Update(quest);
        }
    }
}
