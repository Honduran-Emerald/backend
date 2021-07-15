using Emerald.Domain.Models.ModuleAggregate.RequestEvents;
using Emerald.Domain.Models.ModuleAggregate.ResponseEvents;
using Emerald.Domain.Models.TrackerAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.ModuleAggregate.Modules
{
    public class ChoiceModule : Module
    {
        public List<Choice> Choices { get; set; }

        public ChoiceModule(ObjectId id, string objective, List<Choice> choices)
            : base(id, objective)
        {
            Choices = choices;
        }

        private ChoiceModule() : base()
        {
            Choices = default!;
        }

        public void AddChoice(Module module, string choice)
        {
            if (Choices.Any(c => c.ModuleId == module.Id))
            {
                throw new DomainException("Can not add already existing component");
            }

            Choices.Add(new Choice(module.Id, choice));
        }

        public void RemoveChoice(Module module)
        {
            if (Choices.Any(c => c.ModuleId == module.Id) == false)
            {
                throw new DomainException("Can not remove missing component");
            }

            Choices.Remove(Choices
                .Where(c => c.ModuleId == module.Id)
                .First());
        }

        public override ResponseEventCollection ProcessEvent(TrackerNodeMemento? memento, RequestEvent requestEvent)
        {
            if (requestEvent is ChoiceRequestEvent choiceEvent &&
                choiceEvent.Choice < Choices.Count)
            {
                return new ResponseEventCollection(
                    new ChoiceModuleMemento(choiceEvent.Choice),
                    new List<IResponseEvent>
                    {
                        new ModuleFinishResponseEvent(Choices[choiceEvent.Choice].ModuleId),
                        new ExperienceResponseEvent((long)(200 * (2 + new Random().NextDouble())))
                    });
            }
            else
            {
                return new ResponseEventCollection(
                    memento,
                    new List<IResponseEvent>());
            }
        }

        public class Choice
        {
            public ObjectId ModuleId { get; set; }
            public string Text { get; set; }

            public Choice(ObjectId moduleId, string text)
            {
                ModuleId = moduleId;
                Text = text;
            }
        }
    }
}
