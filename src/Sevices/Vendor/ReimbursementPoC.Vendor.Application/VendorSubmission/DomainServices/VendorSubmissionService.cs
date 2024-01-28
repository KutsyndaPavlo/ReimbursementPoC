using ReimbursementPoC.Vendor.Application.Common.Interfaces;
using ReimbursementPoC.Vendor.Domain.VendorSubmission.DomainServices;

namespace ReimbursementPoC.Vendor.Application.VendorSubmission.DomainServices
{
    public class VendorSubmissionService : IVendorSubmissionService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public VendorSubmissionService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public bool CheckIfVendorSubmissionExists(Guid serviceId, Guid vendorId)
        {
            
            // ToDo add specification
            return _applicationDbContext.VendorSubmissions.Any(x => x.VendorId == vendorId && x.ServiceId == serviceId);
        }
    }
}
