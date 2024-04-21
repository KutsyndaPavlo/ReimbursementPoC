namespace ReimbursementPoC.Administration.IntergrationEvents
{
    public record ProgramUpdatedIntegrationEvent(Guid Id, string Name, string Description);
}
