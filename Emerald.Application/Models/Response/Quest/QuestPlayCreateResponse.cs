using Emerald.Application.Models.Quest.Tracker;

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
