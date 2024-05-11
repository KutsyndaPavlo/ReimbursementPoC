using Elastic.Clients.Elasticsearch;
using MassTransit;
using ReimbursementPoC.Administration.IntergrationEvents;
using ReimbursementPoC.Vendor.IntergrationEvents;
using ReimbursementPoC.VendorSearch.API.IntegrationEventHandlers.Program;

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

    public class ServiceCanceledIntegrationEventConsumerDefinition : ConsumerDefinition<ServiceCanceledIntegrationEventConsumer>
    {
        protected override void ConfigureConsumer(
            IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<ServiceCanceledIntegrationEventConsumer> consumerConfigurator)
        {
            consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(5)));
        }
    }
}
