using Emerald.Domain.Models.ModuleAggregate.RequestEvents;
using Emerald.Domain.Models.ModuleAggregate.ResponseEvents;
using Emerald.Domain.Models.TrackerAggregate;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.ModuleAggregate.Modules
{
    public class ChoiceModule : Module
    {
        public List<string> Choices { get; private set; }
        public List<ObjectId> ChoiceModuleIds { get; private set; }

        public ChoiceModule(string title, List<Module> choices) : base(title)
        {
            ChoiceModuleIds = choices.Select(m => m.Id).ToList();
        }

        public ChoiceModule() : base()
        {
        }

        public void AddChoice(Module module, string choice)
        {
            if (ChoiceModuleIds.Contains(module.Id))
            {
                throw new DomainException("Can not add already existing component");
            }

            Choices.Add(choice);
            ChoiceModuleIds.Add(module.Id);
        }

        public void RemoveChoice(Module module)
        {
            if (ChoiceModuleIds.Contains(module.Id) == false)
            {
                throw new DomainException("Can not remove missing component");
            }

            int index = ChoiceModuleIds.IndexOf(module.Id);

            Choices.RemoveAt(index);
            ChoiceModuleIds.RemoveAt(index);
        }

        public override ResponseEventCollection ProcessEvent(TrackerNodeMemento memento, RequestEvent requestEvent)
        {
            if (requestEvent is ChoiceRequestEvent choiceEvent &&
                choiceEvent.Choice < ChoiceModuleIds.Count)
            {
                return new ResponseEventCollection(
                    new ChoiceModuleMemento(choiceEvent.Choice),
                    new List<IResponseEvent>
                    {
                        new ModuleFinishResponseEvent(ChoiceModuleIds[choiceEvent.Choice]),
                        new ExperienceResponseEvent(200)
                    });
            }
            else
            {
                return new ResponseEventCollection(
                    memento,
                    new List<IResponseEvent>());
            }
        }
    }
}
