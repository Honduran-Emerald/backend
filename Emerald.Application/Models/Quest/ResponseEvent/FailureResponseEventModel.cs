using Emerald.Application.Models.Quest.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.ResponseEvent
{
    public class FailureResponseEventModel : ResponseEventModel
    {
        public bool Close { get; set; }

        public FailureResponseEventModel(bool close)
            : base(ResponseEventType.Failure)
        {
            Close = close;
        }
    }
}
