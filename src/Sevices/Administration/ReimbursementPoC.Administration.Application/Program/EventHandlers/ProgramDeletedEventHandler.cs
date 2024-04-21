using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Administration.Domain.Program.Events;
using ReimbursementPoC.Administration.IntergrationEvents;

namespace ReimbursementPoC.Administration.Application.Program.EventHandlers
{
    public class ProgramDeletedEventHandler : INotificationHandler<DomainEventNotification<ProgramDeletedEvent>>
    {
        private readonly ILogger<ProgramDeletedEventHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProgramDeletedEventHandler(ILogger<ProgramDeletedEventHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(DomainEventNotification<ProgramDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            var integrationEvent = new ProgramDeletedIntegrationEvent(domainEvent.Program.Id);

            await _publishEndpoint.Publish(integrationEvent);

            domainEvent.IsPublished = true;
        }
    }
}
