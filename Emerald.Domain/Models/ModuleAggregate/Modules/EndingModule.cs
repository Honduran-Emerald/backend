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
    public class EndingModule : Module
    {
        public float EndingFactor { get; set; }

        public EndingModule(ObjectId id, string objective, float endingFactor) : base(id, objective)
        {
            EndingFactor = endingFactor;
        }

        private EndingModule()
        {
            EndingFactor = default!;
        }

        public override ResponseEventCollection ProcessEvent(TrackerNodeMemento? memento, RequestEvent requestEvent)
        {
            if (requestEvent is ChoiceRequestEvent)
            {
                return new ResponseEventCollection(
                    memento,
                    new List<IResponseEvent>
                    {
                        new QuestFinishResponseEvent(EndingFactor),
                        new ExperienceResponseEvent((long) (2000 * EndingFactor))
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
