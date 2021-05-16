using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.ModuleMemento
{
    public class ChoiceMementoModel : MementoModel
    {
        public int Choice { get; set; }

        public ChoiceMementoModel(int choice) : base(MementoType.Choice)
        {
            Choice = choice;
        }
    }
}
