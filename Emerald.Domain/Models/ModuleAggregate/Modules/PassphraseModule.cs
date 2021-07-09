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
    public class PassphraseModule : Module
    {
        public ObjectId NextModuleId { get; set; }
        public string Text { get; set; }

        public PassphraseModule(ObjectId id, string objective, string text, ObjectId nextModuleId)
            : base(id, objective)
        {
            Text = text;
            NextModuleId = nextModuleId;
        }

        public override ResponseEventCollection ProcessEvent(TrackerNodeMemento? memento, RequestEvent requestEvent)
            => requestEvent is TextRequestEvent textEvent
            ? textEvent.Text == Text
            ? new ResponseEventCollection(
                    new TextModuleMemento(textEvent.Text),
                    new List<IResponseEvent>
                    {
                        new ModuleFinishResponseEvent(NextModuleId),
                        new ExperienceResponseEvent(150)
                    })
            : new ResponseEventCollection(
                    memento,
                    new List<IResponseEvent>
                    {
                        new FailureResponseEvent(false)
                    })
            : new ResponseEventCollection(
                        memento,
                        new List<IResponseEvent>());
    }
}
