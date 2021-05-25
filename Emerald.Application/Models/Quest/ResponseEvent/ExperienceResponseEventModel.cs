using Emerald.Application.Models.Quest.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.ResponseEvent
{
    public class ExperienceResponseEventModel : ResponseEventModel
    {
        public long Experience { get; set; }

        public ExperienceResponseEventModel(long experience) : base(ResponseEventType.Experience)
        {
            Experience = experience;
        }

        public ExperienceResponseEventModel()
        {
            Experience = default!;
        }
    }
}
