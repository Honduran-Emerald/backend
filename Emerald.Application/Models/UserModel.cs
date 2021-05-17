using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }

        public int Level { get; set; }
        public long Experience { get; set; }
        public int Glory { get; set; }

        public int QuestCount { get; set; }
        public int TrackerCount { get; set; }
    }
}
