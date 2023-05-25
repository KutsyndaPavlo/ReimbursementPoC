using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Program.Domain.Program.Events;

namespace ReimbursementPoC.Program.Application.Program.EventHandlers
{
    public class ProgramDeletedEventHandler : INotificationHandler<DomainEventNotification<ProgramDeletedEvent>>
    {
        private readonly ILogger<ProgramDeletedEventHandler> _logger;

        public ProgramDeletedEventHandler(ILogger<ProgramDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ProgramDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
