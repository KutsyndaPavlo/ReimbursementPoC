using ReimbursementPoC.Administration.Domain.Common;
using System.Linq.Expressions;

namespace ReimbursementPoC.Administration.Domain.Program.Specifications
{
    public class ProgramIdNotEqualsSpecification : Specification<ProgramEntity>
    {
        private readonly Guid _id;

        public ProgramIdNotEqualsSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<ProgramEntity, bool>> ToExpression()
        {
            return (item) => (item.Id != _id);
        }
    }
}
