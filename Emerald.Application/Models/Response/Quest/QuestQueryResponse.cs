using Emerald.Application.Models.Quest;
using System.Collections.Generic;

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
