using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Vendor.Domain.Vendor.Events;
using ReimbursementPoC.Vendor.IntergrationEvents;

namespace ReimbursementPoC.Vendor.Application.VendorSubmission.EventHandlers
{
    public class VendorSubmissionCanceledEventHandler : INotificationHandler<DomainEventNotification<VendorSubmissionCanceledEvent>>
    {
        private readonly ILogger<VendorSubmissionCanceledEventHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public VendorSubmissionCanceledEventHandler(ILogger<VendorSubmissionCanceledEventHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(DomainEventNotification<VendorSubmissionCanceledEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            var integrationEvent = new VendorSubmissionCanceledIntegrationEvent(domainEvent.VendorSubmission.Id);

            await _publishEndpoint.Publish(integrationEvent).ConfigureAwait(false);

            domainEvent.IsPublished = true;

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




internal class VendorSubmissionCreatedEventHandler : INotificationHandler<DomainEventNotification<VendorSubmissionCreatedEvent>>
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

        var integrationEvent = new VendorSubmissionCanceledIntegrationEvent(notification.DomainEvent.VendorSubmission.Id);

        await _publishEndpoint.Publish(integrationEvent).ConfigureAwait(false);

        domainEvent.IsPublished = true;
    }
}
