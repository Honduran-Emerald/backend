using Emerald.Application.Models.Quest;
using Emerald.Application.Models.Quest.Module;
using Emerald.Domain.Models.PrototypeAggregate;
using Emerald.Domain.Models.QuestAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Response.Quest
{
    public class QuestCreateGetResponse
    {
        public QuestPrototype QuestPrototype { get; set; }

        public QuestCreateGetResponse(QuestPrototype questPrototype)
        {
            QuestPrototype = questPrototype;
        }
    }
}
