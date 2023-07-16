using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Administration.Domain.Service.Events;

namespace ReimbursementPoC.Service.Application.Service.EventHandlers
{
    public class ServiceDeactivatedEventHandler : INotificationHandler<DomainEventNotification<ServiceDeactivatedEvent>>
    {
        private readonly ILogger<ServiceDeactivatedEventHandler> _logger;

        public ServiceDeactivatedEventHandler(ILogger<ServiceDeactivatedEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(DomainEventNotification<ServiceDeactivatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);
           
        }
    }
}
