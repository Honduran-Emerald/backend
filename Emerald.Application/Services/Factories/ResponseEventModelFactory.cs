using Emerald.Application.Models.Quest.Events;
using Emerald.Application.Models.Quest.Module;
using Emerald.Application.Models.Quest.ResponseEvent;
using Emerald.Application.Models.Response;
using Emerald.Application.Services.Factories;
using Emerald.Domain.Models;
using Emerald.Domain.Models.ModuleAggregate;
using Emerald.Domain.Models.ModuleAggregate.ResponseEvents;
using Emerald.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emerald.Application.Services
{
    public class ResponseEventModelFactory : IModelFactory<ResponseEventCollection, ResponseEventCollectionModel>
    {
        private IModuleRepository moduleRepository;
        private MementoModelFactory mementoModelFactory;
        private ModuleModelFactory moduleModelFactory;

        public ResponseEventModelFactory(IModuleRepository moduleRepository, MementoModelFactory mementoModelFactory, ModuleModelFactory moduleModelFactory)
        {
            this.moduleRepository = moduleRepository;
            this.mementoModelFactory = mementoModelFactory;
            this.moduleModelFactory = moduleModelFactory;
        }

        public async Task<ResponseEventCollectionModel> Create(ResponseEventCollection responseEventCollection)
        {
            List<ResponseEventModel> responseEvents = new List<ResponseEventModel>();

            foreach (IResponseEvent responseEvent in responseEventCollection.Events)
            {
                switch (responseEvent)
                {
                    case ExperienceResponseEvent experienceEvent:
                        responseEvents.Add(new ExperienceResponseEventModel(
                            experienceEvent.Experience));

                        break;
                    case ModuleFinishResponseEvent moduleFinishEvent:
                        responseEvents.Add(new ModuleFinishResponseEventModel(
                            await moduleModelFactory.Create(
                                await moduleRepository.Get(moduleFinishEvent.ModuleId))));

                        break;
                    default:
                        throw new Exception($"Got invalid ResponseEvent ({responseEvent.GetType().Name})");
                }
            }

            return new ResponseEventCollectionModel(
                responseEvents: responseEvents,
                memento: responseEventCollection.Memento == null
                ? null
                : await mementoModelFactory.Create(responseEventCollection.Memento));
        }
    }
}
