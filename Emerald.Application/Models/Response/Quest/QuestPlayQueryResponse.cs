using Emerald.Application.Models.Quest.Tracker;
using System.Collections.Generic;

namespace Emerald.Application.Models.Response.Quest
{
    public class QuestPlayQueryResponse
    {
        public List<TrackerModel> Trackers { get; set; }

        public QuestPlayQueryResponse(List<TrackerModel> trackers)
        {
            Trackers = trackers;
        }

        private QuestPlayQueryResponse()
        {
            Trackers = default!;
        }
    }
}
