using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Vendor.Domain.Vendor.Events;

namespace ReimbursementPoC.Vendor.Application.VendorSubmission.EventHandlers
{
    internal class VendorCreatedEventHandler : INotificationHandler<DomainEventNotification<VendorSubmissionCreatedEvent>>
    {
        private readonly ILogger<VendorCreatedEventHandler> _logger;

        public VendorCreatedEventHandler(ILogger<VendorCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<VendorSubmissionCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
