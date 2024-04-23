using Elastic.Clients.Elasticsearch;
using MassTransit;
using ReimbursementPoC.Administration.IntergrationEvents;

namespace ReimbursementPoC.VendorSearch.API.IntegrationEventHandlers.Program
{
    public class ProgramUpdatedIntegrationEventConsumer() : IConsumer<ProgramUpdatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<ProgramUpdatedIntegrationEvent> context)
        {
            var client = new ElasticsearchClient(new Uri($"http://{Environment.GetEnvironmentVariable("ElasticSearchHost") ?? "localhost"}:9200"));

            // create index
            var indexName = "vendor_submission_index";
            var res = await client.Indices.ExistsAsync(indexName);

            if (!res.Exists)
            {
                await client.Indices.CreateAsync(indexName);
            };

            var response = await client.UpdateAsync<ProgramUpdatedIntegrationEvent, object>(
                indexName,
                context.Message.Id,
                u => u.Doc(new
                {
                    Service = new
                    {
                        Program = new
                        {
                            context.Message.Name,
                            context.Message.Description
                        }
                    }
                }));
        }
    }
}
