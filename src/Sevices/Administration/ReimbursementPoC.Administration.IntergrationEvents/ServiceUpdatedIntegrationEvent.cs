namespace ReimbursementPoC.Administration.IntergrationEvents
{
    public record ServiceUpdatedIntegrationEvent(Guid Id, string Name, string Description, bool IsCanceled);
}
