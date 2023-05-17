using PriceAnalytics.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using ReimbursementPoC.Program.Domain.Program.Events;
using ReimbursementPoC.Program.Domain.Product.Events;

namespace ReimbursementPoC.Program.Application.Program.EventHandlers
{
    internal class ProgramUpdatedEventHandler : INotificationHandler<DomainEventNotification<ProgramUpdatedEvent>>
    {
        private readonly ILogger<ProgramUpdatedEventHandler> _logger;

        public ProgramUpdatedEventHandler(ILogger<ProgramUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ProgramUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
