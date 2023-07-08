using ReimbursementPoC.Customer.Domain.Common;
using ReimbursementPoC.Customer.Domain.Customer;

namespace ReimbursementPoC.Customer.Domain.Product.Rules
{
    public class CustomerSubmissionNameMustBeUniqueRule : IBusinessRule
    {
        //private readonly ICustomerService _programUniquenessChecker;

        //private readonly string _name;

        //public CustomerNameMustBeUniqueRule(
        //    ICustomerService productUniquenessChecker,
        //    string name)
        //{
        //    _programUniquenessChecker = productUniquenessChecker;
        //    _name = name;
        //}

        //public bool IsBroken() => !_programUniquenessChecker.IsUniqueName(_name);

        //public string Message => "Customer with this name already exists.";
        public string Message => throw new NotImplementedException();

        public bool IsBroken()
        {
            throw new NotImplementedException();
        }
    }
}
