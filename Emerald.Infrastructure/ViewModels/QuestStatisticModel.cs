using MongoDB.Bson;

namespace Emerald.Infrastructure.ViewModels
{
    public class QuestStatisticModel
    {
        public ObjectId QuestId { get; private set; }

        public int Votes { get; private set; }
        public int Finishs { get; private set; }
        public int Plays { get; private set; }

        public QuestStatisticModel(ObjectId questId, int votes, int finishs, int plays)
        {
            QuestId = questId;
            Votes = votes;
            Finishs = finishs;
            Plays = plays;
        }

        private QuestStatisticModel()
        {
        }
    }
}
