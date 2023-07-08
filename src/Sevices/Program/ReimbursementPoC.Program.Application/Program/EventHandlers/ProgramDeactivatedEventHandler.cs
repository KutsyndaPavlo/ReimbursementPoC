using PriceAnalytics.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using ReimbursementPoC.Program.Application.Common.Interfaces;
using ReimbursementPoC.Program.Domain.Program.Events;

namespace ReimbursementPoC.Program.Application.Program.EventHandlers
{
    public class ProgramDeactivatedEventHandler : INotificationHandler<DomainEventNotification<ProgramDeactivatedEvent>>
    {
        private readonly ILogger<ProgramDeactivatedEventHandler> _logger;
        private readonly IApplicationDbContext _applicationDbContext;

        public ProgramDeactivatedEventHandler(ILogger<ProgramDeactivatedEventHandler> logger,
                                              IApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        public async Task Handle(DomainEventNotification<ProgramDeactivatedEvent> notification, CancellationToken cancellationToken)
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
