using ReimbursementPoC.Vendor.Domain.Common;
using ReimbursementPoC.Vendor.Domain.VendorSubmission.DomainServices;

namespace ReimbursementPoC.Vendor.Domain.Product.Rules
{
    public class VendorSubmissionMustBeSinglePerServiceRule : IBusinessRule
    {
        private readonly Guid _serviceId;
        private readonly Guid _vendorId;
        private readonly IVendorSubmissionService _vendorSubmissionService;

        public VendorSubmissionMustBeSinglePerServiceRule(
            Guid serviceId,
            Guid vendorId,
            IVendorSubmissionService vendorSubmissionService)
        {
            _serviceId = serviceId;
            _vendorId = vendorId;
            _vendorSubmissionService = vendorSubmissionService;
        }

        public bool IsBroken() => _vendorSubmissionService.CheckIfVendorSubmissionExists(_serviceId, _vendorId);

        public string Message => "Vendor submission already exist within the service.";
    }
}
