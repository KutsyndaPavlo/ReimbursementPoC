﻿using ReimbursementPoC.Program.Domain.Common;
using ReimbursementPoC.Program.Domain.Service;

namespace ReimbursementPoC.Program.Domain.Service.Events
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