using Emerald.Domain.Models.ModuleAggregate.RequestEvents;
using Emerald.Domain.Models.ModuleAggregate.ResponseEvents;
using Emerald.Domain.Models.TrackerAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.ModuleAggregate.Modules
{
    public class StoryModule : Module
    {
        public ObjectId NextModuleId { get; private set; }

        public StoryModule(ObjectId nextModuleId)
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
