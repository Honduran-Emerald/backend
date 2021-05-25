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
            QuestVersion questVersion = quest.PublishQuestVersion(questPrototype);
            QuestPrototypeContext context = new QuestPrototypeContext(questPrototype);

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

            if (moduleIds.Count > 0)
            {
                questVersion.PlaceModules(
                    moduleIds,
                    context.ConvertModuleId(questPrototype.FirstModuleId));
            }

            await questRepository.Update(quest);
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

        public ObjectId ConvertModuleId(int moduleId)
            => ModuleIds[moduleId];
    }
}
