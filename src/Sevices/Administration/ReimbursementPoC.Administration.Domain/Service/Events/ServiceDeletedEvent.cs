using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Service;

namespace ReimbursementPoC.Administration.Domain.Service.Events
{
    public class ServiceDeletedEvent : DomainEvent
    {
        public ServiceDeletedEvent(ServiceEntity service)
        {
            Service = service;
        }

        public ServiceEntity Service { get; }
    }
}