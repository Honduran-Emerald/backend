using Emerald.Application.Models.Quest.Events;
using Emerald.Application.Models.Quest.ModuleMemento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Response
{
    public class ResponseEventCollectionModel
    {
        public List<ResponseEventModel> ResponseEvents { get; set; }
        public MementoModel? Memento { get; set; }

        public ResponseEventCollectionModel(List<ResponseEventModel> responseEvents, MementoModel? memento)
        {
            ResponseEvents = responseEvents;
            Memento = memento;
        }

        public ResponseEventCollectionModel()
        {
            ResponseEvents = default!;
        }
    }
}
