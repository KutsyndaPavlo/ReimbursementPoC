using Elastic.Clients.Elasticsearch;
using MassTransit;
using ReimbursementPoC.Vendor.IntergrationEvents;

namespace ReimbursementPoC.VendorSearch.API.IntegrationEventHandlers.VendorSubmission
{
    public class VendorSubmissionCreatedIntegrationEventConsumer() : IConsumer<VendorSubmissionCreatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<VendorSubmissionCreatedIntegrationEvent> context)
        {
            var client = new ElasticsearchClient(new Uri($"{Environment.GetEnvironmentVariable("ElasticSearchHost") ?? "localhost"}:9200"));

            // create index
            var indexName = "vendor_submission_index";
            var res = await client.Indices.ExistsAsync(indexName);
            if (!res.Exists)
            {
                await client.Indices.CreateAsync(indexName);
            };

            var request = new IndexRequest<VendorSubmissionCreatedIntegrationEvent>(context.Message, indexName = indexName, context.Message.Id);
            await client.IndexAsync(request);


            //var item = new ProductProposal()
            //{
            //    Currency = @event.Currency,
            //    ProductName = @event.ProductName,
            //    Date = @event.Date,
            //    Description = @event.Description,
            //    Price = @event.Price,
            //    ProductCode = @event.ProductCode,
            //    ProductId = @event.ProductId,
            //    SellerId = @event.SellerId,
            //    SellerName = @event.SellerName,
            //    Id = @event.Id,
            //};

            //await _respository.AddItemAsync(item);

            await Task.CompletedTask;
        }
    }
}
