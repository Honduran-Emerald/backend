using MongoDB.Bson;

namespace Emerald.Infrastructure.ViewModels
{
    public class QuestViewModel
    {
        public ObjectId QuestId { get; set; }

        public int Votes { get; set; }
        public int Finishes { get; set; }
        public int Plays { get; set; }

        public QuestViewModel(ObjectId questId, int votes, int finishs, int plays)
        {
            QuestId = questId;
            Votes = votes;
            Finishes = finishs;
            Plays = plays;
        }

        private QuestViewModel()
        {
        }
    }
}
