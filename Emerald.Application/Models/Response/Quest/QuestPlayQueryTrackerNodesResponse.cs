using Emerald.Application.Models.Quest.Tracker;
using Emerald.Domain.Models.TrackerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Response.Quest
{
    public class QuestPlayQueryTrackerNodesResponse
    {
        public List<TrackerNodeModel> TrackerNodes { get; set; }

        public QuestPlayQueryTrackerNodesResponse(List<TrackerNodeModel> trackerNodes)
        {
            TrackerNodes = trackerNodes;
        }

        public QuestPlayQueryTrackerNodesResponse()
        {
            TrackerNodes = default!;
        }
    }
}
