using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Administration.Domain.Program.Events;
using ReimbursementPoC.Administration.IntergrationEvents;
using ReimbursementPoC.Infrustructure.EventBus.Abstractions;

namespace ReimbursementPoC.Administration.Application.Program.EventHandlers
{
    internal class ProgramCreatedEventHandler : INotificationHandler<DomainEventNotification<ProgramCreatedEvent>>
    {
        private readonly ILogger<ProgramCreatedEventHandler> _logger;
        private readonly IEventBus _eventBus;

        public ProgramCreatedEventHandler(ILogger<ProgramCreatedEventHandler> logger, IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }

        public Task Handle(DomainEventNotification<ProgramCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            var integrationEvent = new ProgramCreatedIntegrationEvent
            { 
                ProgramId = domainEvent.Program.Id,
            };

            //_eventBus.Publish(integrationEvent);

            //await Task.Delay(TimeSpan.FromSeconds(1));
            domainEvent.IsPublished = true;

            return Task.CompletedTask;
        }
    }
}
