using Emerald.Domain.Models.QuestPrototypeAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Response.Quest
{
    public class QuestCreatePutResponse
    {
        public List<ImagePrototype> Images { get; set; }

        public QuestCreatePutResponse(List<ImagePrototype> images)
        {
            Images = images;
        }
    }
}
