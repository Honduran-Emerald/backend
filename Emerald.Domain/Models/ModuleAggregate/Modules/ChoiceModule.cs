using Emerald.Domain.Models.ModuleAggregate.RequestEvents;
using Emerald.Domain.Models.ModuleAggregate.ResponseEvents;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.ModuleAggregate.Modules
{
    public class ChoiceModule : Module
    {
        public List<ObjectId> ChoiceModuleIds { get; set; }

        public ChoiceModule(string title, List<Module> choices) : base(title)
        {
            ChoiceModuleIds = choices.Select(m => m.Id).ToList();
        }

        public void AddChoice(Module module)
        {
            if (ChoiceModuleIds.Contains(module.Id))
            {
                throw new DomainException("Can not add already existing component");
            }

            ChoiceModuleIds.Add(module.Id);
        }

        public void RemoveChoice(Module module)
        {
            if (ChoiceModuleIds.Contains(module.Id) == false)
            {
                throw new DomainException("Can not remove missing component");
            }

            ChoiceModuleIds.Remove(module.Id);
        }

        public override ResponseEvent ProcessEvent(RequestEvent requestEvent)
        {
            switch (requestEvent)
            {
                case ChoiceEvent choiceEvent when choiceEvent.Choice < ChoiceModuleIds.Count:
                    return new NextModuleEvent(
                        new ChoiceModuleMemento(choiceEvent.Choice),
                        ChoiceModuleIds[choiceEvent.Choice]);

                default:
                    return new IdleEvent(requestEvent.Memento);
            }
        }
    }
}
