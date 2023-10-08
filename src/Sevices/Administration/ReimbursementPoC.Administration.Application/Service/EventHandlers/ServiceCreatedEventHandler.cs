using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Administration.Domain.Service.Events;

namespace ReimbursementPoC.Service.Application.Service.EventHandlers
{
    internal class ServiceCreatedEventHandler : INotificationHandler<DomainEventNotification<ServiceCreatedEvent>>
    {
        private readonly ILogger<ServiceCreatedEventHandler> _logger;

        public ServiceCreatedEventHandler(ILogger<ServiceCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ServiceCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
