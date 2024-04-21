using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Vendor.Domain.Vendor.Events;
using ReimbursementPoC.Vendor.IntergrationEvents;

namespace ReimbursementPoC.Vendor.Application.Vendor.EventHandlers
{
    public class VendorSubmissionDeletedEventHandler : INotificationHandler<DomainEventNotification<VendorSubmissionDeletedEvent>>
    {
        private readonly ILogger<VendorSubmissionDeletedEventHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public VendorSubmissionDeletedEventHandler(ILogger<VendorSubmissionDeletedEventHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(DomainEventNotification<VendorSubmissionDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            var integrationEvent = new VendorSubmissionDeletedIntegrationEvent(notification.DomainEvent.VendorSubmission.Id);

            await _publishEndpoint.Publish(integrationEvent);

            domainEvent.IsPublished = true;
        }
    }
}
