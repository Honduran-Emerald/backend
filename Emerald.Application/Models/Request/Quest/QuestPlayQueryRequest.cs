using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public QuestPlayQueryRequest()
        {
            Unfinished = false;
        }
    }
}
