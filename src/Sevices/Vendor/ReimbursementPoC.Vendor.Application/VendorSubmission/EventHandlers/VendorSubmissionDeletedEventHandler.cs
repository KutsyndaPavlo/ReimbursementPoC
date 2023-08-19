using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Vendor.Domain.Vendor.Events;

namespace ReimbursementPoC.Vendor.Application.Vendor.EventHandlers
{
    public class VendorDeletedEventHandler : INotificationHandler<DomainEventNotification<VendorSubmissionDeletedEvent>>
    {
        private readonly ILogger<VendorDeletedEventHandler> _logger;

        public VendorDeletedEventHandler(ILogger<VendorDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<VendorSubmissionDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
