using ReimbursementPoC.Vendor.Domain.Common;

namespace ReimbursementPoC.Vendor.Domain.Product.Rules
{
    public class VendorSubmissionMustBeSinglePerServiceRule : IBusinessRule
    {
        private readonly Guid _serviceId;

        public VendorSubmissionMustBeSinglePerServiceRule(Guid serviceId)
        {
            _serviceId = serviceId;
        }

        public bool IsBroken() => false;  //ToDo

        public string Message => "Vendor submission already exist within the service.";
    }
}
