using ReimbursementPoC.Program.Domain.Common;

namespace ReimbursementPoC.Program.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
