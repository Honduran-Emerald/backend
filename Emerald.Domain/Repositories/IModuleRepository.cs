﻿using Emerald.Domain.Models.ModuleAggregate;
using MongoDB.Bson;
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
