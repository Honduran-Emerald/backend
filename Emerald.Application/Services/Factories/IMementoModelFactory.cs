using Emerald.Application.Models.Quest.ModuleMemento;
using Emerald.Domain.Models.TrackerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public interface IMementoModelFactory
    {
        Task<MementoModel> Create(TrackerPathMemento memento);
    }
}
