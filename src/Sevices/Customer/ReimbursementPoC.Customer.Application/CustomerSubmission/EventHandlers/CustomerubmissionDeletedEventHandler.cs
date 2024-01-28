using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Customer.Domain.Customer.Events;

namespace ReimbursementPoC.Customer.Application.Customer.EventHandlers
{
    public class CustomerDeletedEventHandler : INotificationHandler<DomainEventNotification<CustomerSubmissionDeletedEvent>>
    {
        private readonly ILogger<CustomerDeletedEventHandler> _logger;

        public CustomerDeletedEventHandler(ILogger<CustomerDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<CustomerSubmissionDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
