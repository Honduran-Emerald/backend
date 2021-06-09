using Emerald.Application.Models.Quest.Component;
using Emerald.Application.Models.Quest.Module;
using Emerald.Domain.Models.ComponentAggregate;
using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.ModuleAggregate.Modules;
using Emerald.Infrastructure.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public class ModuleModelFactory : IModelFactory<Module, ModuleModel>
    {
        private ComponentModelFactory componentModelFactory;
        private IComponentRepository componentRepository;

        public ModuleModelFactory(ComponentModelFactory componentModelFactory, IComponentRepository componentRepository)
        {
            this.componentModelFactory = componentModelFactory;
            this.componentRepository = componentRepository;
        }

        public async Task<ModuleModel> Create(Module module)
        {
            List<ComponentModel> componentModels = await ComponentModelsFromModule(module);

            switch (module)
            {
                case ChoiceModule choiceModule:
                    return new ChoiceModuleModel(
                        module.Id.ToString(),
                        module.Objective,
                        Enumerable.Range(0, choiceModule.Choices.Count)
                            .Select(i => new ChoiceModuleModel.ChoiceModuleModelChoice(
                                text: choiceModule.Choices[i].Text))
                            .ToList(),
                        componentModels);

                case StoryModule storyModule:
                    return new StoryModuleModel(
                        module.Id.ToString(),
                        module.Objective,
                        componentModels);

                case EndingModule endingModule:
                    return new EndingModuleModel(
                        module.Id.ToString(),
                        module.Objective,
                        componentModels,
                        endingModule.EndingFactor);
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
