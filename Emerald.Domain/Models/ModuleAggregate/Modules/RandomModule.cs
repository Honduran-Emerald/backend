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
    public class RandomModule : Module
    {
        public ObjectId NextLeftModuleId { get; set; }
        public ObjectId NextRightModuleId { get; set; }
        public float LeftRatio { get; set; }

        public RandomModule(ObjectId id, string objective, ObjectId nextLeftModuleId, ObjectId nextRightModuleId, float leftRatio)
            : base(id, objective)
        {
            NextLeftModuleId = nextLeftModuleId;
            NextRightModuleId = nextRightModuleId;
            LeftRatio = leftRatio;
        }

        public override ResponseEventCollection ProcessEvent(TrackerNodeMemento? memento, RequestEvent requestEvent)
        {
            Random random = new Random();
            bool isLeft = random.NextDouble() < LeftRatio;

            return requestEvent is ChoiceRequestEvent choiceEvent
                ? new ResponseEventCollection(
                        new ChoiceModuleMemento(choiceEvent.Choice),
                        new List<IResponseEvent>
                        {
                            new ModuleFinishResponseEvent(isLeft ? NextLeftModuleId : NextRightModuleId),
                            new ExperienceResponseEvent((long) (100 / Math.Clamp(0.05 + (isLeft ? (LeftRatio) : (1 - LeftRatio)), 0, 1)))
                        })
                : new ResponseEventCollection(
                            memento,
                            new List<IResponseEvent>());
        }
    }
}
