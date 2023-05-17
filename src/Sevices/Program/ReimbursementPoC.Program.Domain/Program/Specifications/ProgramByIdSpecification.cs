using ReimbursementPoC.Program.Domain.Common;
using ReimbursementPoC.Program.Domain.Program;
using System.Linq.Expressions;

namespace PriceAnalytics.Administration.Domain.Product.Specification
{
    public class ProgramByIdSpecification : Specification<ProgramEntity>
    {
        private readonly Guid _id;

        public ProgramByIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<ProgramEntity, bool>> ToExpression()
        {
            return (item) => (item.Id == _id);
        }
    }
}