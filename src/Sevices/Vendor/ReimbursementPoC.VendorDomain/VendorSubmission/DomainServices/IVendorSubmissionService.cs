namespace ReimbursementPoC.Vendor.Domain.VendorSubmission.DomainServices
{
    public interface IVendorSubmissionService
    {
        bool CheckIfVendorSubmissionExists(Guid serviceId, Guid vendorId);
    }
}
