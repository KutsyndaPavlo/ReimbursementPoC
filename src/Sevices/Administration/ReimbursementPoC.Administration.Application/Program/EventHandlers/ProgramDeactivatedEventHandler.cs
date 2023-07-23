using PriceAnalytics.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using ReimbursementPoC.Administration.Application.Common.Interfaces;
using ReimbursementPoC.Administration.Domain.Program.Events;

namespace ReimbursementPoC.Administration.Application.Program.EventHandlers
{
    public class ProgramDeactivatedEventHandler : INotificationHandler<DomainEventNotification<ProgramCanceledEvent>>
    {
        private readonly ILogger<ProgramDeactivatedEventHandler> _logger;
        private readonly IApplicationDbContext _applicationDbContext;

        public ProgramDeactivatedEventHandler(ILogger<ProgramDeactivatedEventHandler> logger,
                                              IApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        public async Task Handle(DomainEventNotification<ProgramCanceledEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);
           
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
