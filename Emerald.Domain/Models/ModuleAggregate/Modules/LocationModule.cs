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
    public class LocationModule : Module
    {
        public ObjectId NextModuleId { get; set; }
        public Location Location { get; set; }

        public LocationModule(ObjectId id, string objective, ObjectId nextModuleId, Location location)
            : base(id, objective)
        {
            NextModuleId = nextModuleId;
            Location = location;
        }

        public override ResponseEventCollection ProcessEvent(TrackerNodeMemento? memento, RequestEvent requestEvent)
        {
            switch (requestEvent)
            {
                case ChoiceRequestEvent choiceEvent:
                    return new ResponseEventCollection(
                        memento,
                        new List<IResponseEvent>
                        {
                            new ExperienceResponseEvent(500),
                            new ModuleFinishResponseEvent(NextModuleId)
                        });

                default:
                    return new ResponseEventCollection(
                        memento,
                        new List<IResponseEvent>());
            }
        }
    }
}
