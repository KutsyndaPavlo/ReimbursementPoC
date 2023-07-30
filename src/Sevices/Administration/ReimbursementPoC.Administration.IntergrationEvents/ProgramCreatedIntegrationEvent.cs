using ReimbursementPoC.Infrustructure.EventBus.Events;

namespace ReimbursementPoC.Administration.IntergrationEvents
{
    public record ProgramCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid ProgramId { get; set; }
    }
}
