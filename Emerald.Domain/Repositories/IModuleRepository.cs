using Emerald.Domain.Models.ModuleAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Repositories
{
    public interface IModuleRepository
    {
        Task Add(Module module);
        Task<Module> Get(ObjectId id);
        Task Update(Module module);
    }
}
