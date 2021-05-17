using Emerald.Application.Models.Quest.Component;
using Emerald.Application.Models.Quest.Module;
using Emerald.Domain.Models.ComponentAggregate;
using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.ModuleAggregate.Modules;
using Emerald.Infrastructure.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public class ModuleModuleFactory : IModuleModelFactory
    {
        private IComponentModelFactory componentModelFactory;
        private IComponentRepository componentRepository;

        public async Task<ModuleModel> Create(Module module)
        {
            List<ComponentModel> componentModels = await ComponentModelsFromModule(module);

            switch (module)
            {
                case ChoiceModule choiceModule:
                    return new ChoiceModuleModel(choiceModule.Id.ToString(), choiceModule.Choices, componentModels);
            }

            throw new Exception("Got invalid Module");
        }

        private async Task<List<ComponentModel>> ComponentModelsFromModule(Module module)
        {
            List<ComponentModel> componentModels = new List<ComponentModel>();

            foreach (ObjectId componentId in module.ComponentIds)
            {
                Component component = await componentRepository.Get(componentId);
                componentModels.Add(await componentModelFactory.Create(component));
            }

            return componentModels;
        }
    }
}
