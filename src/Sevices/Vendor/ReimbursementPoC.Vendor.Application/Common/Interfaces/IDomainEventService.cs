using ReimbursementPoC.Vendor.Domain.Common;

namespace ReimbursementPoC.Vendor.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
