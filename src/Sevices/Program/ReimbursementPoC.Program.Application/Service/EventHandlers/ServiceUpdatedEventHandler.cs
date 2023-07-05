using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Program.Domain.Service.Events;

namespace ReimbursementPoC.Service.Application.Service.EventHandlers
{
    internal class ServiceUpdatedEventHandler : INotificationHandler<DomainEventNotification<ServiceUpdatedEvent>>
    {
        private readonly ILogger<ServiceUpdatedEventHandler> _logger;

        public ServiceUpdatedEventHandler(ILogger<ServiceUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ServiceUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
