using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.ElasticModels
{
    public class QuestElasticModel
    {
        public string Id { get; set; }
        public string? Title { get; set; }

        public List<string>? Tags { get; set; }
        public GeoLocation? Location { get; set; }
        public DateTime? CreationTime { get; set; }

        public int Votes { get; set; }
        public int Plays { get; set; }
        public float FinishRate { get; set; }

        public QuestElasticModel(string id, string? title, List<string>? tags, GeoLocation? location, DateTime? creationTime, int votes, int plays, float finishRate)
        {
            Id = id;
            Title = title;
            Tags = tags;
            Location = location;
            CreationTime = creationTime;
            Votes = votes;
            Plays = plays;
            FinishRate = finishRate;
        }
    }
}
