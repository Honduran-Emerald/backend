using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Request
{
    public class QuestPlayQueryRequest
    {
        public bool Unfinished { get; set; }

        public QuestPlayQueryRequest(bool unfinished)
        {
            Unfinished = unfinished;
        }

        private QuestPlayQueryRequest()
        {
            Unfinished = false;
        }
    }
}
