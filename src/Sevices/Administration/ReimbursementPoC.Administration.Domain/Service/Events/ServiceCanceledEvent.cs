using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Service;

namespace ReimbursementPoC.Administration.Domain.Service.Events
{
    public class ServiceCanceledEvent : DomainEvent
    {
        public ServiceCanceledEvent(ServiceEntity service)
        {
            Service = service;
        }

        public ServiceEntity Service { get; }
    }
}
