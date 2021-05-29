using Emerald.Domain.Models.ComponentAggregate;
using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.PrototypeAggregate;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestPrototypeAggregate;
using Emerald.Domain.Models.QuestVersionAggregate;
using Emerald.Domain.Repositories;
using Emerald.Infrastructure.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Services
{
    public class QuestCreateService
    {
        private IModuleRepository moduleRepository;
        private IComponentRepository componentRepository;
        private IQuestRepository questRepository;
        private IQuestPrototypeRepository questPrototypeRepository;

        public QuestCreateService(IModuleRepository moduleRepository, IComponentRepository componentRepository, IQuestRepository questRepository, IQuestPrototypeRepository questPrototypeRepository)
        {
            this.moduleRepository = moduleRepository;
            this.componentRepository = componentRepository;
            this.questRepository = questRepository;
            this.questPrototypeRepository = questPrototypeRepository;
        }

        public async Task Publish(Quest quest)
        {
            QuestPrototype questPrototype = await questPrototypeRepository.Get(quest.PrototypeId);
            QuestPrototypeContext context = new QuestPrototypeContext(questPrototype);

            if (questPrototype.Modules.Count == 0)
            {
                throw new DomainException("Quest needs at least 1 module to be published");
            }

            List<ObjectId> moduleIds = new List<ObjectId>();

            foreach (ModulePrototype modulePrototype in questPrototype.Modules)
            {
                Module module = modulePrototype.ConvertToModule(context);

                foreach (ComponentPrototype componentPrototype in modulePrototype.Components)
                {
                    Component component = componentPrototype.ConvertToComponent();

                    module.AddComponent(component);
                    await componentRepository.Add(component);
                }

                moduleIds.Add(module.Id);
                await moduleRepository.Add(module);
            }

            quest.PublishQuestVersion(
                questPrototype,
                moduleIds,
                context.ConvertModuleId(questPrototype.FirstModuleId));

            await questRepository.Update(quest);
        }

        public void Verify(QuestPrototype questPrototype)
        {
            questPrototype.Verify(new QuestPrototypeContext(questPrototype));
        }
    }

    internal class QuestPrototypeContext : IPrototypeContext
    {
        private Dictionary<int, ObjectId> ModuleIds;

        public QuestPrototypeContext(QuestPrototype questPrototype)
        {
            ModuleIds = new Dictionary<int, ObjectId>();
            ModuleIds = questPrototype.Modules
                .ToDictionary(m => m.Id, _ => ObjectId.GenerateNewId());
        }

        public bool ContainsModuleId(int moduleId)
            => ModuleIds.ContainsKey(moduleId);

        public ObjectId ConvertModuleId(int moduleId)
            => ModuleIds[moduleId];
    }
}
