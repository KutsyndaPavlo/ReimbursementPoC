using ReimbursementPoC.Customer.Application.Common.Interfaces;
using ReimbursementPoC.Customer.Domain.CustomerSubmission.DomainServices;

namespace ReimbursementPoC.Customer.Application.CustomerSubmission.DomainServices
{
    public class CustomerSubmissionService : ICustomerSubmissionService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public CustomerSubmissionService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public bool CheckIfCustomerSubmissionExists(Guid vendorSubmissionId, Guid customerId)
        {
            // ToDo add specification
            return _applicationDbContext.CustomerSubmissions
                .Any(x => x.CustomerId == customerId && x.VendorSubmissionId == vendorSubmissionId);
        }
    }
}
