using Emerald.Application.Models.Quest.ModuleMemento;
using Emerald.Domain.Models.ModuleAggregate.Modules;
using Emerald.Domain.Models.TrackerAggregate;
using System;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public class MementoModelFactory : IModelFactory<TrackerNodeMemento?, MementoModel?>
    {
        public async Task<MementoModel?> Create(TrackerNodeMemento? memento)
        {
            if (memento == null)
                return null;

            switch (memento)
            {
                case ChoiceModuleMemento choiceMemento:
                    return new ChoiceMementoModel(choiceMemento.Choice);
            }

            throw new Exception("Got invalid TrackerPathMemento");
        }
    }
}
