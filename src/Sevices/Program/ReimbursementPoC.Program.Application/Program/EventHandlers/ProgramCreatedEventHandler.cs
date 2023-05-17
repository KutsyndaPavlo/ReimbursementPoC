using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Program.Domain.Product.Events;

namespace ReimbursementPoC.Program.Application.Program.EventHandlers
{
    internal class ProgramCreatedEventHandler : INotificationHandler<DomainEventNotification<ProgramCreatedEvent>>
    {
        private readonly ILogger<ProgramCreatedEventHandler> _logger;

        public ProgramCreatedEventHandler(ILogger<ProgramCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ProgramCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
