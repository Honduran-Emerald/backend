using Emerald.Application.Models.Quest.Component;
using Emerald.Domain.Models.ComponentAggregate;
using System;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public class ComponentModelFactory : IModelFactory<Component, ComponentModel>
    {
        public async Task<ComponentModel> Create(Component component)
        {
            throw new Exception("Got invalid Component");
        }
    }
}
