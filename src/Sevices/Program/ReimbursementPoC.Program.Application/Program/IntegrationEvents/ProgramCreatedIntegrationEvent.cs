using ReimbursementPoC.Infrustructure.EventBus.Events;

namespace ReimbursementPoC.Program.Application.Program.IntegrationEvents
{
    public record ProgramCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid ProgramId { get; set; }
    }
}
