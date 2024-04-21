namespace ReimbursementPoC.Vendor.IntergrationEvents
{
    public record VendorSubmissionCreatedIntegrationEvent(
        Guid Id,
        Vendor Vendor,
        Service Service, 
        string? Description,
        bool IsCanceled);

    public record Vendor(Guid Id, string Name);
    public record Service(Guid Id, string Name, string Description, Program Program, bool IsCanceled);
    public record Program(Guid Id, string Name, string Description, string State, DateTime StartDate, DateTime EndDate, bool IsCanceled);
}