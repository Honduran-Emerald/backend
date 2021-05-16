using Emerald.Application.Models.Quest.Events;
using Emerald.Application.Models.Quest.Module;
using Emerald.Application.Services.Factories;
using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.ModuleAggregate.ResponseEvents;
using Emerald.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services
{
    public class ResponseEventModelFactory : IResponseEventModelFactory
    {
        private IModuleRepository moduleRepository;
        private IModuleModelFactory moduleModelFactory;

        public ResponseEventModelFactory(IModuleRepository moduleRepository, IModuleModelFactory moduleModelFactory)
        {
            this.moduleRepository = moduleRepository;
            this.moduleModelFactory = moduleModelFactory;
        }

        public async Task<ResponseEventModel> Create(ResponseEvent responseEvent)
        {
            switch (responseEvent)
            {
                case IdleResponseEvent idleEvent:
                    return new IdleResponseEventModel();

                case NextModuleResponseEvent nextModuleEvent:
                    ModuleModel moduleModel = await moduleModelFactory.Create(
                        await moduleRepository.Get(nextModuleEvent.ModuleId));

                    return new NextModuleResponseEventModel(moduleModel);
            }

            throw new Exception("Got invalid ResponseEvent");
        }
    }
}
