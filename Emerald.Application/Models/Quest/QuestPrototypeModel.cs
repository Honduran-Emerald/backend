using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest
{
    public class QuestPrototypeModel
    {
        public string Title { get; set; }
        public string ImageId { get; set; }

        public QuestPrototypeModel(string title, string imageId)
        {
            Title = title;
            ImageId = imageId;
        }
    }
}
