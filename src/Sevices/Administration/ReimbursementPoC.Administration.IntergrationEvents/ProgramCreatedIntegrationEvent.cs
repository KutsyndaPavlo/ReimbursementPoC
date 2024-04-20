namespace ReimbursementPoC.Administration.IntergrationEvents
{
    public record ProgramCreatedIntegrationEvent
    {
        public Guid ProgramId { get; set; }
    }
}
