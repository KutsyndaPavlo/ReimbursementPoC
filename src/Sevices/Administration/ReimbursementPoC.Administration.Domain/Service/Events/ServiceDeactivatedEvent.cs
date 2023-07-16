using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Service;

namespace ReimbursementPoC.Administration.Domain.Service.Events
{
    public class ServiceDeactivatedEvent : DomainEvent
    {
        public ServiceDeactivatedEvent(ServiceEntity service)
        {
            Service = service;
        }

        public ServiceEntity Service { get; }
    }
}
