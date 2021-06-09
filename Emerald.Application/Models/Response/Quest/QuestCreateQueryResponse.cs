using Emerald.Application.Models.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
