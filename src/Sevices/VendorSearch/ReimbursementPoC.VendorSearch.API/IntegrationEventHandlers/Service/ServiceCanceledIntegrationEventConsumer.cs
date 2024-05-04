using Elastic.Clients.Elasticsearch;
using MassTransit;
using ReimbursementPoC.Administration.IntergrationEvents;
using ReimbursementPoC.Vendor.IntergrationEvents;

namespace ReimbursementPoC.VendorSearch.API.IntegrationEventHandlers.Service
{
    public class ServiceCanceledIntegrationEventConsumer() : IConsumer<ServiceCanceledIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<ServiceCanceledIntegrationEvent> context)
        {
            var client = new ElasticsearchClient(new Uri($"http://{Environment.GetEnvironmentVariable("ElasticSearchHost") ?? "localhost"}:9200"));

            // create index
            var indexName = "vendor_submission_index";
            var res = await client.Indices.ExistsAsync(indexName);

            if (!res.Exists)
            {
                await client.Indices.CreateAsync(indexName);
            };

            var response = await client.UpdateAsync<VendorSubmissionCreatedIntegrationEvent, object>(
                indexName,
                context.Message.Id,
                u => u.Doc(new 
                {
                    Service = new 
                    { 
                        IsCanceled = true
                    } 
                }));
        }
    }
}
