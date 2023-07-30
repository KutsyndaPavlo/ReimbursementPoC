using ReimbursementPoC.Administration.Domain.Common;

namespace ReimbursementPoC.Administration.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
