using ReimbursementPoC.Vendor.Domain.Common;
using ReimbursementPoC.Vendor.Domain.Vendor;
using System.Linq.Expressions;

namespace ReimbursementPoC.Vendor.Domain.VendorSubmission.Specification
{
    public class VendorSubmissionByIdSpecification : Specification<VendorSubmissionEntity>
    {
        private readonly Guid _id;

        public VendorSubmissionByIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<VendorSubmissionEntity, bool>> ToExpression()
        {
            return (item) => (item.Id == _id);
        }
    }
}