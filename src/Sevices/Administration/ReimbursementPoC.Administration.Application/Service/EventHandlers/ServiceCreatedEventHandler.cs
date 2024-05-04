using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Administration.Domain.Service.Events;
using ReimbursementPoC.Administration.IntergrationEvents;

namespace ReimbursementPoC.Service.Application.Service.EventHandlers
{
    internal class ServiceCreatedEventHandler : INotificationHandler<DomainEventNotification<ServiceCreatedEvent>>
    {
        private readonly ILogger<ServiceCreatedEventHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public ServiceCreatedEventHandler(ILogger<ServiceCreatedEventHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(DomainEventNotification<ServiceCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            var integrationEvent = new ServiceCreatedIntegrationEvent(
                domainEvent.Service.Id,
                domainEvent.Service.Name,
                domainEvent.Service.Description ?? "",
                domainEvent.Service.IsCanceled,
                domainEvent.Service.ProgramId);

            await _publishEndpoint.Publish(integrationEvent);

            domainEvent.IsPublished = true;
        }
    }
}
