using PriceAnalytics.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using ReimbursementPoC.Customer.Application.Common.Interfaces;
using ReimbursementPoC.Customer.Domain.Customer.Events;

namespace ReimbursementPoC.Customer.Application.CustomerSubmission.EventHandlers
{
    public class CustomerDeactivatedEventHandler : INotificationHandler<DomainEventNotification<CustomerSubmissionDeactivatedEvent>>
    {
        private readonly ILogger<CustomerDeactivatedEventHandler> _logger;
        private readonly IApplicationDbContext _applicationDbContext;

        public CustomerDeactivatedEventHandler(ILogger<CustomerDeactivatedEventHandler> logger,
                                              IApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        public async Task Handle(DomainEventNotification<CustomerSubmissionDeactivatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);
           
            //await DeactivateProposals(domainEvent, cancellationToken);
        }

        //private async Task DeactivateProposals(CustomerDeactivatedEvent domainEvent, CancellationToken cancellationToken)
        //{
        //    foreach (var item in _applicationDbContext.Proposals.Where(new ActiveProposalsForCustomerSpecification(domainEvent.Customer.Id).ToExpression()))
        //    {
        //        item.DeActivate();
        //    }

        //    await _applicationDbContext.SaveChangesAsync(cancellationToken);
        //}
    }
}
