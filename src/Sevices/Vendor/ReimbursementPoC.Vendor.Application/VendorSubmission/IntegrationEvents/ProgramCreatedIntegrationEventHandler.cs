using MassTransit;
using ReimbursementPoC.Administration.IntergrationEvents;

namespace ReimbursementPoC.Vendor.Application.VendorSubmission.IntegrationEvents
{
    public class ProgramCreatedIntegrationEventConsumer() : IConsumer<ProgramCreatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<ProgramCreatedIntegrationEvent> context)
        {
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