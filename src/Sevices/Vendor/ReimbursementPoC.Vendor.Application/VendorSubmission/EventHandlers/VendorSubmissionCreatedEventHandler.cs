using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Vendor.Domain.Vendor.Events;
using ReimbursementPoC.Vendor.IntergrationEvents;

namespace ReimbursementPoC.Vendor.Application.VendorSubmission.EventHandlers
{
    public class VendorSubmissionCreatedEventHandler : INotificationHandler<DomainEventNotification<VendorSubmissionCreatedEvent>>
    {
        private readonly ILogger<VendorSubmissionCreatedEventHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public VendorSubmissionCreatedEventHandler(ILogger<VendorSubmissionCreatedEventHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(DomainEventNotification<VendorSubmissionCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            var vs = notification.DomainEvent.VendorSubmission;

            var integrationEvent = new VendorSubmissionCreatedIntegrationEvent(
                vs.Id,
                vs.VendorId,
                vs.VendorName,
                vs.ServiceId,
                vs.ServiceFullName,
                vs.Description,
                vs.IsCanceled);

            await _publishEndpoint.Publish(integrationEvent).ConfigureAwait(false);

            domainEvent.IsPublished = true;
        }
    }
}
