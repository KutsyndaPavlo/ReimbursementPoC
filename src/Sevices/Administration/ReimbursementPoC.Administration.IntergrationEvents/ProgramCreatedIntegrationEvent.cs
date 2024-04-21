namespace ReimbursementPoC.Administration.IntergrationEvents
{
    public record ProgramCreatedIntegrationEvent(Guid Id, string Name, string Description, string State, DateTime StartDate, DateTime EndDate);
}


