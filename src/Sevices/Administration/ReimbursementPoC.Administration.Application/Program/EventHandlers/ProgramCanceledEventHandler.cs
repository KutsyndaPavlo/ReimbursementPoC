using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Administration.Domain.Program.Events;
using ReimbursementPoC.Administration.IntergrationEvents;

namespace ReimbursementPoC.Administration.Application.Program.EventHandlers
{
    public class ProgramCanceledEventHandler : INotificationHandler<DomainEventNotification<ProgramCanceledEvent>>
    {
        private readonly ILogger<ProgramCanceledEventHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProgramCanceledEventHandler(ILogger<ProgramCanceledEventHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(DomainEventNotification<ProgramCanceledEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            var integrationEvent = new ProgramCanceledIntegrationEvent
            {
            };

            await _publishEndpoint.Publish(integrationEvent);

            domainEvent.IsPublished = true;

            //await DeactivateProposals(domainEvent, cancellationToken);
        }

        //private async Task DeactivateProposals(ProgramDeactivatedEvent domainEvent, CancellationToken cancellationToken)
        //{
        //    foreach (var item in _applicationDbContext.Proposals.Where(new ActiveProposalsForProgramSpecification(domainEvent.Program.Id).ToExpression()))
        //    {
        //        item.DeActivate();
        //    }

        //    await _applicationDbContext.SaveChangesAsync(cancellationToken);
        //}
    }
}
