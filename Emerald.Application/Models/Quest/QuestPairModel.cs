using Emerald.Domain.Models.QuestVersionAggregate;

namespace Emerald.Application.Models.Quest
{
    public class QuestPairModel
    {
        public Domain.Models.QuestAggregate.Quest Quest { get; set; }
        public QuestVersion? QuestVersion { get; set; }

        public QuestPairModel(Domain.Models.QuestAggregate.Quest quest, QuestVersion? questVersion)
        {
            Quest = quest;
            QuestVersion = questVersion;
        }
    }
}
