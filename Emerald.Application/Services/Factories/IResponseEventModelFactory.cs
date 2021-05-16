using Emerald.Application.Models.Quest.Events;
using Emerald.Domain.Models.ModuleAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services
{
    public interface IResponseEventModelFactory
    {
        Task<ResponseEventModel> Create(ResponseEvent responseEvent);
    }
}
