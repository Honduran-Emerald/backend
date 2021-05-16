using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.Events
{
    public class IdleResponseEventModel : ResponseEventModel
    {
        public IdleResponseEventModel() : base(ResponseEventType.Idle)
        {
        }
    }
}
