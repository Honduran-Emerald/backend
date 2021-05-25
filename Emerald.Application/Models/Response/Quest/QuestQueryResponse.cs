using Emerald.Application.Models.Quest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Response.Quest
{
    public class QuestQueryResponse
    {
        public List<QuestModel> Quests { get; set; }

        public QuestQueryResponse(List<QuestModel> quests)
        {
            Quests = quests;
        }

        private QuestQueryResponse()
        {
            Quests = default!;
        }
    }
}
