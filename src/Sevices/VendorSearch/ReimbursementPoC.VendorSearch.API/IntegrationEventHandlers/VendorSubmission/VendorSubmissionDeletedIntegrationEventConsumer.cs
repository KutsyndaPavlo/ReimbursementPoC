using Elastic.Clients.Elasticsearch;
using MassTransit;
using ReimbursementPoC.Vendor.IntergrationEvents;

namespace ReimbursementPoC.VendorSearch.API.IntegrationEventHandlers.VendorSubmission
{
    public class VendorSubmissionDeletedIntegrationEventConsumer() : IConsumer<VendorSubmissionDeletedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<VendorSubmissionDeletedIntegrationEvent> context)
        {
            var client = new ElasticsearchClient(new Uri($"http://{Environment.GetEnvironmentVariable("ElasticSearchHost") ?? "localhost"}:9200"));

            // create index
            var indexName = "vendor_submission_index";
            var res = await client.Indices.ExistsAsync(indexName);
            if (!res.Exists)
            {
                await client.Indices.CreateAsync(indexName);
            };

            var request = new DeleteRequest(indexName, context.Message.Id);
            await client.DeleteAsync(request);
        }
    }
}
