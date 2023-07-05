using ReimbursementPoC.Vendor.Domain.Common;
using ReimbursementPoC.Vendor.Domain.Vendor;

namespace ReimbursementPoC.Vendor.Domain.Product.Rules
{
    public class VendorSubmissionNameMustBeUniqueRule : IBusinessRule
    {
        //private readonly IVendorService _programUniquenessChecker;

        //private readonly string _name;

        //public VendorNameMustBeUniqueRule(
        //    IVendorService productUniquenessChecker,
        //    string name)
        //{
        //    _programUniquenessChecker = productUniquenessChecker;
        //    _name = name;
        //}

        //public bool IsBroken() => !_programUniquenessChecker.IsUniqueName(_name);

        //public string Message => "Vendor with this name already exists.";
        public string Message => throw new NotImplementedException();

        public bool IsBroken()
        {
            throw new NotImplementedException();
        }
    }
}
