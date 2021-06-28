using Emerald.Domain.Models.QuestAggregate;
using Emerald.Infrastructure.ElasticModels;
using Emerald.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Services
{
    public class ElasticService : IElasticService
    {
        private ElasticClient elasticClient;

        public ElasticService(IConfiguration configuration)
        {
            ConnectionSettings connection = new ConnectionSettings(new Uri("http://localhost:9200")); // configuration["ElasticSearchConnection"]

            connection.DefaultMappingFor<QuestElasticModel>(q =>
                q.IdProperty(q => q.Id)
                 .IndexName("quests"));

            connection.DefaultMappingFor<Quest>(q =>
                q.IdProperty(q => q.Id)
                 .IndexName("quests2"));

            elasticClient = new ElasticClient(connection);
            
        }

        public IElasticClient ElasticClient => elasticClient;
    }

    public static class ElasticSearchExtensions
    {
        public static async Task CreateElasticIndices(this IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            using (var serviceScope = scopeFactory.CreateScope())
            {
                var elasticClient = serviceScope.ServiceProvider.GetRequiredService<IElasticService>().ElasticClient;

                if (elasticClient.Indices.Exists("quests").Exists == false)
                {
                    var questRepository = serviceScope.ServiceProvider.GetRequiredService<IQuestRepository>();
                    var factory = serviceScope.ServiceProvider.GetRequiredService<QuestElasticModelFactory>();

                    foreach (Quest quest in questRepository.GetQueryable())
                    {
                        var result = elasticClient.IndexDocument(await factory.Create(quest));

                        if (result.IsValid == false)
                        {
                            var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<ElasticService>>();
                            logger.LogInformation(result.DebugInformation);
                        }
                    }
                }
            }
        }
    }

    public static class ElasticClientExtensions
    {
        public static async Task IndexDocumentOrThrow<T>(this IElasticClient elasticClient, T t) where T : class
        {
            var result = await elasticClient.IndexDocumentAsync(t);
            
            if (result.IsValid == false)
            {
                throw new Exception(result.DebugInformation);
            }
        }
    }
}
