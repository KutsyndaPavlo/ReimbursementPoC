using Elastic.Clients.Elasticsearch;
using MassTransit;
using ReimbursementPoC.Vendor.IntergrationEvents;

namespace ReimbursementPoC.VendorSearch.API.IntegrationEventHandlers.VendorSubmission
{
    public class VendorSubmissionCreatedIntegrationEventConsumer() : IConsumer<VendorSubmissionCreatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<VendorSubmissionCreatedIntegrationEvent> context)
        {
            var client = new ElasticsearchClient(new Uri($"http://{Environment.GetEnvironmentVariable("ElasticSearchHost") ?? "localhost"}:9200"));

            // create index
            var indexName = "vendor_submission_index";
            var res = await client.Indices.ExistsAsync(indexName);
            if (!res.Exists)
            {
                await client.Indices.CreateAsync(indexName);
            };

            var request = new IndexRequest<VendorSubmissionCreatedIntegrationEvent>(context.Message, indexName, context.Message.Id);
            await client.IndexAsync(request);
        }
    }

    public class VendorSubmissionCreatedIntegrationEventConsumerDefinition : ConsumerDefinition<VendorSubmissionCreatedIntegrationEventConsumer>
    {
        protected override void ConfigureConsumer(
            IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<VendorSubmissionCreatedIntegrationEventConsumer> consumerConfigurator)
        {
            consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(5)));
        }
    }
}
