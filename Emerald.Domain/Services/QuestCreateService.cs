using Emerald.Domain.Models.ComponentAggregate;
using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.PrototypeAggregate;
using Emerald.Domain.Models.QuestAggregate;
using Emerald.Domain.Models.QuestPrototypeAggregate;
using Emerald.Domain.Repositories;
using Emerald.Infrastructure.Repositories;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
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

        public async Task Publish(QuestPrototype questPrototype, Quest quest, IPrototypeContext prototypeContext)
        {
            Verify(questPrototype);

            if (questPrototype.Modules.Count == 0)
            {
                throw new DomainException("Quest needs at least 1 module to be published");
            }

            List<ObjectId> moduleIds = new List<ObjectId>();

            foreach (ModulePrototype modulePrototype in questPrototype.Modules)
            {
                Module module = modulePrototype.ConvertToModule(prototypeContext);

                foreach (ComponentPrototype componentPrototype in modulePrototype.Components)
                {
                    Component component = componentPrototype.ConvertToComponent(prototypeContext);

                    module.AddComponent(component);
                    await componentRepository.Add(component);
                }

                moduleIds.Add(module.Id);
                await moduleRepository.Add(module);
            }

            quest.PublishQuestVersion(
                questPrototype,
                moduleIds,
                prototypeContext.ConvertModuleId((int)questPrototype.FirstModuleReference!));

            await questRepository.Update(quest);
        }

        public void Verify(QuestPrototype questPrototype)
        {
            questPrototype.Verify();
        }
    }
}
