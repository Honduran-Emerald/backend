using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.ModuleMemento
{
    public class EmptyMementoModel : MementoModel
    {
        public EmptyMementoModel() : base(MementoType.Empty)
        {
        }
    }
}
