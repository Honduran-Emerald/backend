using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.Events
{
    public class ResponseEventModel
    {
        ResponseEventType Type { get; set; }
        

        public ResponseEventModel(ResponseEventType type)
        {
            Type = type;
        }
    }
}
