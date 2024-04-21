namespace ReimbursementPoC.Administration.IntergrationEvents
{
    public record ServiceCreatedIntegrationEvent(Guid Id, string Name, string Description, bool IsCanceled, Guid ProgramId);
}