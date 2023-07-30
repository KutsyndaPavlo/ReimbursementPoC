using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Service;

namespace ReimbursementPoC.Administration.Domain.Service.Events
{
    public class ServiceCreatedEvent : DomainEvent
    {
        public ServiceCreatedEvent(ServiceEntity service)
        {
            Service = service;
        }

        public ServiceEntity Service { get; }
    }
}