namespace ReimbursementPoC.Vendor.IntergrationEvents
{
    public record VendorSubmissionCreatedIntegrationEvent(
        Guid Id,
        Guid VendorId, 
        string VendorName, 
        Guid ServiceId, 
        string ServiceFullName, 
        string? Description,
        bool IsCanceled);
}
