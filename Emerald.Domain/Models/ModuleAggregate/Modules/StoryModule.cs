using Emerald.Domain.Models.ModuleAggregate.RequestEvents;
using Emerald.Domain.Models.ModuleAggregate.ResponseEvents;
using Emerald.Domain.Models.TrackerAggregate;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Emerald.Domain.Models.ModuleAggregate.Modules
{
    public class StoryModule : Module
    {
        public ObjectId NextModuleId { get; set; }

        public StoryModule(ObjectId id, string objective, ObjectId nextModuleId)
            : base(id, objective)
        {
            NextModuleId = nextModuleId;
        }

        public override ResponseEventCollection ProcessEvent(TrackerNodeMemento? memento, RequestEvent requestEvent)
            => requestEvent is ChoiceRequestEvent choiceEvent
            ? new ResponseEventCollection(
                    new ChoiceModuleMemento(choiceEvent.Choice),
                    new List<IResponseEvent>
                    {
                        new ModuleFinishResponseEvent(NextModuleId),
                        new ExperienceResponseEvent(150)
                    })
            : new ResponseEventCollection(
                        memento,
                        new List<IResponseEvent>());
    }
}
