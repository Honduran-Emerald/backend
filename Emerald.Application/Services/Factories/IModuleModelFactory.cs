using Emerald.Application.Models.Quest.Module;
using Emerald.Domain.Models.ModuleAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public interface IModuleModelFactory
    {
        Task<ModuleModel> Create(Module module);
    }
}
