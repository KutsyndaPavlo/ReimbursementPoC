using Elastic.Clients.Elasticsearch;
using MassTransit;
using ReimbursementPoC.Vendor.IntergrationEvents;

namespace ReimbursementPoC.VendorSearch.API.IntegrationEventHandlers.VendorSubmission
{
    public class VendorSubmissionCanceledIntegrationEventConsumer() : IConsumer<VendorSubmissionCanceledIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<VendorSubmissionCanceledIntegrationEvent> context)
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
                u => u.Doc(new { IsCanceled  = true }));
        }
    }

    public class VendorSubmissionCanceledIntegrationEventConsumerDefinition : ConsumerDefinition<VendorSubmissionCanceledIntegrationEventConsumer>
    {
        protected override void ConfigureConsumer(
            IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<VendorSubmissionCanceledIntegrationEventConsumer> consumerConfigurator)
        {
            consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(5)));
        }
    }
}
