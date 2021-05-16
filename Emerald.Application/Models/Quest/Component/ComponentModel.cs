using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.Component
{
    public class ComponentModel
    {
        public ComponentType ComponentType { get; set; }

        public ComponentModel(ComponentType componentType)
        {
            ComponentType = componentType;
        }
    }
}
