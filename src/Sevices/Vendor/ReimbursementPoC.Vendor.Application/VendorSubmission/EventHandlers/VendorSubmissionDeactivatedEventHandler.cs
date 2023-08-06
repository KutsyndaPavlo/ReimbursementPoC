using PriceAnalytics.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using ReimbursementPoC.Vendor.Application.Common.Interfaces;
using ReimbursementPoC.Vendor.Domain.Vendor.Events;

namespace ReimbursementPoC.Vendor.Application.VendorSubmission.EventHandlers
{
    public class VendorDeactivatedEventHandler : INotificationHandler<DomainEventNotification<VendorSubmissionCanceledEvent>>
    {
        private readonly ILogger<VendorDeactivatedEventHandler> _logger;
        private readonly IApplicationDbContext _applicationDbContext;

        public VendorDeactivatedEventHandler(ILogger<VendorDeactivatedEventHandler> logger,
                                              IApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        public async Task Handle(DomainEventNotification<VendorSubmissionCanceledEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);
           
            //await DeactivateProposals(domainEvent, cancellationToken);
        }

        //private async Task DeactivateProposals(VendorDeactivatedEvent domainEvent, CancellationToken cancellationToken)
        //{
        //    foreach (var item in _applicationDbContext.Proposals.Where(new ActiveProposalsForVendorSpecification(domainEvent.Vendor.Id).ToExpression()))
        //    {
        //        item.DeActivate();
        //    }

        //    await _applicationDbContext.SaveChangesAsync(cancellationToken);
        //}
    }
}
