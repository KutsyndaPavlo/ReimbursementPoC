using ReimbursementPoC.Customer.Domain.Common;
using ReimbursementPoC.Customer.Domain.CustomerSubmission.DomainServices;

namespace ReimbursementPoC.Customer.Domain.Product.Rules
{
    public class CustomerSubmissionNameMustBeUniqueRule : IBusinessRule
    {
        private readonly ICustomerSubmissionService _customerSubmissionService;
        private readonly Guid _customerId;
        private readonly Guid _vendorSubmissionId;

        public CustomerSubmissionNameMustBeUniqueRule(
            ICustomerSubmissionService customerSubmissionService,
            Guid customerId,
            Guid vendorSubmissionId)
        {
            _customerSubmissionService = customerSubmissionService;
            _customerId = customerId;
            _vendorSubmissionId = vendorSubmissionId;
        }

        public string Message => $"Customer submission over vendor submisssion {_vendorSubmissionId} already exists.";

        public bool IsBroken()
        {
           return _customerSubmissionService.CheckIfCustomerSubmissionExists(_vendorSubmissionId, _customerId);
        }
    }
}
