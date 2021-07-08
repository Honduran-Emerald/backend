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
    public class WideAreaModule : Module
    {
        public ObjectId NextModuleId { get; set; }
        public Location Location { get; set; }
        public float Radius { get; set; }

        public WideAreaModule(ObjectId id, string objective, Location location, float radius, ObjectId moduleId)
            : base(id, objective)
        {
            Location = location;
            Radius = radius;
            NextModuleId = moduleId;
        }

        public override ResponseEventCollection ProcessEvent(TrackerNodeMemento? memento, RequestEvent requestEvent)
            => requestEvent is ChoiceRequestEvent choiceEvent
            ? new ResponseEventCollection(
                    new ChoiceModuleMemento(choiceEvent.Choice),
                    new List<IResponseEvent>
                    {
                        new ModuleFinishResponseEvent(NextModuleId),
                        new ExperienceResponseEvent(300)
                    })
            : new ResponseEventCollection(
                        memento,
                        new List<IResponseEvent>());
    }
}
