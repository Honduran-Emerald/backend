using Emerald.Domain.Models.QuestAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
