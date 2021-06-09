using Emerald.Application.Models.Quest.Events;

namespace Emerald.Application.Models.Quest.ResponseEvent
{
    public class ExperienceResponseEventModel : ResponseEventModel
    {
        public long Experience { get; set; }

        public ExperienceResponseEventModel(long experience) : base(ResponseEventType.Experience)
        {
            Experience = experience;
        }

        public ExperienceResponseEventModel()
        {
            Experience = default!;
        }
    }
}
