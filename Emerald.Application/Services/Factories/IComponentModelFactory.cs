using Emerald.Application.Models.Quest.Component;
using Emerald.Domain.Models.ComponentAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public interface IComponentModelFactory
    {
        Task<ComponentModel> Create(Component component);
    }
}
