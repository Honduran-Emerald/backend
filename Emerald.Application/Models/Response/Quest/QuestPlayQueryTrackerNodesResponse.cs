using Emerald.Domain.Models.TrackerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Response.Quest
{
    public class QuestPlayQueryTrackerNodesResponse
    {
        public List<TrackerNode> TrackerNodes { get; set; }

        public QuestPlayQueryTrackerNodesResponse(List<TrackerNode> trackerNodes)
        {
            TrackerNodes = trackerNodes;
        }

        public QuestPlayQueryTrackerNodesResponse()
        {
            TrackerNodes = default!;
        }
    }
}
