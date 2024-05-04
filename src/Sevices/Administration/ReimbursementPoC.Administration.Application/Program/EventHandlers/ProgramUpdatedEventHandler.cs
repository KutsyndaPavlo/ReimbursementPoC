using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Administration.Domain.Program.Events;
using ReimbursementPoC.Administration.IntergrationEvents;

namespace ReimbursementPoC.Administration.Application.Program.EventHandlers
{
    internal class ProgramUpdatedEventHandler : INotificationHandler<DomainEventNotification<ProgramUpdatedEvent>>
    {
        private readonly ILogger<ProgramUpdatedEventHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProgramUpdatedEventHandler(ILogger<ProgramUpdatedEventHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(DomainEventNotification<ProgramUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            var integrationEvent = new ProgramUpdatedIntegrationEvent(
                domainEvent.Program.Id,
                domainEvent.Program.Name,
                domainEvent.Program.Description ?? "");

            await _publishEndpoint.Publish(integrationEvent);

            domainEvent.IsPublished = true;
        }
    }
}
