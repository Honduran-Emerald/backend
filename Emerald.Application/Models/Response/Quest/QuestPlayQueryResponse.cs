using Emerald.Application.Models.Quest.Tracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            Trackers = new List<TrackerModel>();
        }
    }
}
