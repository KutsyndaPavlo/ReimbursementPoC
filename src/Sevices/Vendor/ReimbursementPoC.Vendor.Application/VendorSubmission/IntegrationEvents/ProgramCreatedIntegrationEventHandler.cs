using ReimbursementPoC.Infrustructure.EventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReimbursementPoC.Vendor.Application.VendorSubmission.IntegrationEvents
{
    public class ProgramCreatedIntegrationEventHandler : IIntegrationEventHandler<ProgramCreatedIntegrationEvent>
    {

        //private readonly IRepository<ProductProposal> _respository;

        public ProgramCreatedIntegrationEventHandler()
        {
            // _respository = respository;
        }

        public async Task Handle(ProgramCreatedIntegrationEvent @event)
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
        }
    }
}