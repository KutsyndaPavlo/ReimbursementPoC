using ReimbursementPoC.Infrustructure.EventBus.Events;

namespace ReimbursementPoC.Administration.Application.Program.IntegrationEvents
{
    public record ProgramCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid ProgramId { get; set; }
    }
}
