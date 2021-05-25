using Emerald.Application.Models.Quest.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Request.Quest
{
    public class QuestCreateUpdateRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public LocationModel Location { get; set; }

        public List<ModuleModel> Modules { get; set; }

        public QuestCreateUpdateRequest(string title, string description, string? image, LocationModel location, List<ModuleModel> modules)
        {
            Title = title;
            Description = description;
            Image = image;
            Location = location;
            Modules = modules;
        }

        public QuestCreateUpdateRequest()
        {
            Title = default!;
            Description = default!;
            Image = default!;
            Location = default!;
            Modules = default!;
        }
    }
}
