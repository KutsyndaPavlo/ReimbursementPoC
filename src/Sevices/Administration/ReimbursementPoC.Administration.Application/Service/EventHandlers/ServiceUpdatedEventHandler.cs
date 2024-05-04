using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Administration.Domain.Service.Events;
using ReimbursementPoC.Administration.IntergrationEvents;

namespace ReimbursementPoC.Service.Application.Service.EventHandlers
{
    internal class ServiceUpdatedEventHandler : INotificationHandler<DomainEventNotification<ServiceUpdatedEvent>>
    {
        private readonly ILogger<ServiceUpdatedEventHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public ServiceUpdatedEventHandler(ILogger<ServiceUpdatedEventHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(DomainEventNotification<ServiceUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            var integrationEvent = new ServiceUpdatedIntegrationEvent(
                domainEvent.Service.Id,
                domainEvent.Service.Name,
                domainEvent.Service.Description ?? "",
                domainEvent.Service.IsCanceled);

            await _publishEndpoint.Publish(integrationEvent);

            domainEvent.IsPublished = true;
        }
    }
}
