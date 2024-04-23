using Elastic.Clients.Elasticsearch;
using MassTransit;
using ReimbursementPoC.Administration.IntergrationEvents;

namespace ReimbursementPoC.VendorSearch.API.IntegrationEventHandlers.Service
{
    public class ServiceUpdatedIntegrationEventConsumer() : IConsumer<ServiceUpdatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<ServiceUpdatedIntegrationEvent> context)
        {
            var client = new ElasticsearchClient(new Uri($"http://{Environment.GetEnvironmentVariable("ElasticSearchHost") ?? "localhost"}:9200"));

            // create index
            var indexName = "vendor_submission_index";
            var res = await client.Indices.ExistsAsync(indexName);

            if (!res.Exists)
            {
                await client.Indices.CreateAsync(indexName);
            };

            var response = await client.UpdateAsync<ServiceUpdatedIntegrationEvent, object>(
                indexName,
                context.Message.Id,
                u => u.Doc(new
                {
                    Service = new
                    {
                        context.Message.Name, 
                        context.Message.Description
                    }
                }));
        }
    }
}
