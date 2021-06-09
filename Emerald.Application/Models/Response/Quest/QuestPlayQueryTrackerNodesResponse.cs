using Emerald.Application.Models.Quest.Tracker;
using System.Collections.Generic;

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
