using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Administration.Domain.Program.Events;
using ReimbursementPoC.Administration.IntergrationEvents;

namespace ReimbursementPoC.Administration.Application.Program.EventHandlers
{
    internal class ProgramCreatedEventHandler : INotificationHandler<DomainEventNotification<ProgramCreatedEvent>>
    {
        private readonly ILogger<ProgramCreatedEventHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProgramCreatedEventHandler(ILogger<ProgramCreatedEventHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(DomainEventNotification<ProgramCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            var integrationEvent = new ProgramCreatedIntegrationEvent(
                domainEvent.Program.Id,
                domainEvent.Program.Name,
                domainEvent.Program.Description ?? "",
                "state",//domainEvent.Program.State,              // ToDo bug
                domainEvent.Program.Period.StartDate,
                domainEvent.Program.Period.EndDate);

            await _publishEndpoint.Publish(integrationEvent);

            domainEvent.IsPublished = true;
        }
    }
}
