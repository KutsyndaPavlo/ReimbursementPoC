using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Program.Domain.Service.Events;

namespace ReimbursementPoC.Service.Application.Service.EventHandlers
{
    public class ServiceDeletedEventHandler : INotificationHandler<DomainEventNotification<ServiceDeletedEvent>>
    {
        private readonly ILogger<ServiceDeletedEventHandler> _logger;

        public ServiceDeletedEventHandler(ILogger<ServiceDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ServiceDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
