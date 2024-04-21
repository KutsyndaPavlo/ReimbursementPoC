using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Administration.Domain.Service.Events;
using ReimbursementPoC.Administration.IntergrationEvents;

namespace ReimbursementPoC.Service.Application.Service.EventHandlers
{
    public class ServiceDeletedEventHandler : INotificationHandler<DomainEventNotification<ServiceDeletedEvent>>
    {
        private readonly ILogger<ServiceDeletedEventHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public ServiceDeletedEventHandler(ILogger<ServiceDeletedEventHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(DomainEventNotification<ServiceDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            var integrationEvent = new ServiceDeletedIntegrationEvent(domainEvent.Service.Id);

            await _publishEndpoint.Publish(integrationEvent);

            domainEvent.IsPublished = true;
        }
    }
}
