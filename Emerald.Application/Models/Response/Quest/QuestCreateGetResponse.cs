using Emerald.Domain.Models.PrototypeAggregate;

namespace Emerald.Application.Models.Response.Quest
{
    public class QuestCreateGetResponse
    {
        public QuestPrototype QuestPrototype { get; set; }

        public QuestCreateGetResponse(QuestPrototype questPrototype)
        {
            QuestPrototype = questPrototype;
        }
    }
}
