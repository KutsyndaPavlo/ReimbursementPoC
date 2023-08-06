using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Application.Common.Models;
using ReimbursementPoC.Customer.Domain.Customer.Events;

namespace ReimbursementPoC.Customer.Application.CustomerSubmission.EventHandlers
{
    internal class CustomerCreatedEventHandler : INotificationHandler<DomainEventNotification<CustomerSubmissionCreatedEvent>>
    {
        private readonly ILogger<CustomerCreatedEventHandler> _logger;

        public CustomerCreatedEventHandler(ILogger<CustomerCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<CustomerSubmissionCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
