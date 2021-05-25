using Emerald.Application.Models.Quest.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.ResponseEvent
{
    public class QuestFinishResponseEventModel : ResponseEventModel
    {
        public float EndingFactor { get; set; }

        public QuestFinishResponseEventModel()
        {
        }

        public QuestFinishResponseEventModel(float endingFactor) : base(ResponseEventType.QuestFinish)
        {
            EndingFactor = endingFactor;
        }
    }
}
