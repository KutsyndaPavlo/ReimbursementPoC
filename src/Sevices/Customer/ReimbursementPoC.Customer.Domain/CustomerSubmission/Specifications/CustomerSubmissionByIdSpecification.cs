using ReimbursementPoC.Customer.Domain.Common;
using ReimbursementPoC.Customer.Domain.Customer;
using System.Linq.Expressions;

namespace ReimbursementPoC.Customer.Domain.CustomerSubmission.Specification
{
    public class CustomerSubmissionByIdSpecification : Specification<CustomerSubmissionEntity>
    {
        private readonly Guid _id;

        public CustomerSubmissionByIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<CustomerSubmissionEntity, bool>> ToExpression()
        {
            return (item) => (item.Id == _id);
        }
    }
}