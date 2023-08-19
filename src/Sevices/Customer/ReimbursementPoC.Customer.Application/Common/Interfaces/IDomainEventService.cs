using ReimbursementPoC.Customer.Domain.Common;

namespace ReimbursementPoC.Customer.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
