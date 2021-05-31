using Emerald.Domain.Models.PrototypeAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Response.Quest
{
    public class QuestCreateCreateResponse
    {
        public QuestPrototype QuestPrototype { get; set; }

        public QuestCreateCreateResponse(QuestPrototype questPrototype)
        {
            QuestPrototype = questPrototype;
        }
    }
}
