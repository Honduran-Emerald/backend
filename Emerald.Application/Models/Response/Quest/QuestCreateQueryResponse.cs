using Emerald.Application.Models.Quest;
using Emerald.Application.Models.Quest.Module;
using Emerald.Domain.Models.QuestAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Response.Quest
{
    public class QuestCreateQueryResponse
    {
        public QuestModel Quest { get; set; }
        public List<ModuleModel> Modules { get; set; }
        public string FirstModuleId { get; set; }
    }
}
