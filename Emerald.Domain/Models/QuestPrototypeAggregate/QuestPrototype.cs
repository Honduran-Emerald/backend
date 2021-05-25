using Emerald.Domain.Models.QuestPrototypeAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.PrototypeAggregate
{
    public class QuestPrototype : Entity
    {
        public Location Location { get; }
        public string Title { get; }
        public string Description { get; }
        public string Image { get; }

        public List<ModulePrototype> Modules { get; }
        public int FirstModuleId { get; }

        public QuestPrototype(Location location, string title, string description, string image)
        {
            Location = location;
            Title = title;
            Description = description;
            Image = image;
            Modules = new List<ModulePrototype>();
        }

        private QuestPrototype()
        {
            Location = default!;
            Title = default!;
            Description = default!;
            Image = default!;
            Modules = default!;
            FirstModuleId = default!;
        }
    }
}
