using ReimbursementPoC.Vendor.Domain.Common;
using ReimbursementPoC.Vendor.Domain.Vendor;

namespace ReimbursementPoC.Vendor.Domain.Product.Rules
{
    public class VendorSubmissionShouldUniquePerServiceRule : IBusinessRule
    {
        private readonly string _service;

        public VendorSubmissionShouldUniquePerServiceRule(string service)
        {
            _service = service;
        }

        public bool IsBroken() => false;

        public string Message => "Vendor submission already exist within the service.";
    }
}
