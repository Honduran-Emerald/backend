using Emerald.Domain.Models.QuestAggregate;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Services
{
    public class ElasticSearchService
    {
        public ElasticSearchService(IConfiguration configuration)
        {
            new ConnectionSettings(new Uri(configuration[]))
                .DefaultMappingFor();

            ElasticClient elasticClient = new ElasticClient();
        }
    }
}
