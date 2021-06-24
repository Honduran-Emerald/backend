using Emerald.Application.Models.Quest;
using Emerald.Application.Models.Quest.Tracker;
using Emerald.Domain.Models.QuestAggregate;
using System.Collections.Generic;

namespace Emerald.Application.Models.Response.Quest
{
    public class QuestPlayQueryTrackerNodesResponse
    {
        public QuestModel Quest { get; set; }
        public List<TrackerNodeModel> TrackerNodes { get; set; }

        public QuestPlayQueryTrackerNodesResponse(QuestModel quest, List<TrackerNodeModel> trackerNodes)
        {
            Quest = quest;
            TrackerNodes = trackerNodes;
        }

        public QuestPlayQueryTrackerNodesResponse()
        {
            Quest = default!;
            TrackerNodes = default!;
        }
    }
}
