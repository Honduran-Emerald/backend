using Emerald.Application.Models.Prototype;
using System.Collections.Generic;

namespace Emerald.Application.Models.Response.Quest
{
    public class QuestCreateQueryResponse
    {
        public List<QuestPrototypeModel> Prototypes { get; set; }

        public QuestCreateQueryResponse(List<QuestPrototypeModel> prototypes)
        {
            Prototypes = prototypes;
        }
    }
}
