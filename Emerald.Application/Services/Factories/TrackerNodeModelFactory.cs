using Emerald.Application.Models.Quest.Tracker;
using Emerald.Domain.Models.TrackerAggregate;
using Emerald.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public class TrackerNodeModelFactory : IModelFactory<TrackerNode, TrackerNodeModel>
    {
        private IModuleRepository moduleRepository;

        private MementoModelFactory mementoModelFactory;
        private ModuleModelFactory moduleModelFactory;

        public TrackerNodeModelFactory(IModuleRepository moduleRepository, MementoModelFactory mementoModelFactory, ModuleModelFactory moduleModelFactory)
        {
            this.moduleRepository = moduleRepository;
            this.mementoModelFactory = mementoModelFactory;
            this.moduleModelFactory = moduleModelFactory;
        }

        public async Task<TrackerNodeModel> Create(TrackerNode source)
        {
            return new TrackerNodeModel(
                await mementoModelFactory.Create(source.Memento),
                await moduleModelFactory.Create(await moduleRepository.Get(source.ModuleId)),
                source.CreatedAt);
        }
    }
}
