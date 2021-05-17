using Emerald.Application.Models.Quest;
using Emerald.Domain.Models.QuestAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public interface IQuestModelFactory
    {
        Task<QuestModel> Create(Quest quest);
    }
}
