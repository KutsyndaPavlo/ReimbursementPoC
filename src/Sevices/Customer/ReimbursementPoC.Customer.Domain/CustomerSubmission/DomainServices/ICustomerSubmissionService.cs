namespace ReimbursementPoC.Customer.Domain.CustomerSubmission.DomainServices
{
    public interface ICustomerSubmissionService
    {
        bool CheckIfCustomerSubmissionExists(Guid vendorSubmissionId, Guid customerId);
    }
}
