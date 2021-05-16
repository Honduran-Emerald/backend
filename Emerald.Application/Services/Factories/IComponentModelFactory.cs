using Emerald.Application.Models.Quest.Component;
using Emerald.Domain.Models.ComponentAggregate;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public interface IComponentModelFactory
    {
        Task<ComponentModel> Create(Component component);
    }
}
