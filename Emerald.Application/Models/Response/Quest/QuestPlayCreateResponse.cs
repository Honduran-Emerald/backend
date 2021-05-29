using Emerald.Application.Models.Quest.Module;
using Emerald.Application.Models.Quest.Tracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Response.Quest
{
    public class QuestPlayCreateResponse
    {
        public TrackerModel TrackerModel { get; set; }
        public TrackerNodeModel TrackerNode { get; set; }

        public QuestPlayCreateResponse(TrackerModel trackerModel, TrackerNodeModel trackerNode)
        {
            TrackerModel = trackerModel;
            TrackerNode = trackerNode;
        }
    }
}
