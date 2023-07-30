using ReimbursementPoC.Administration.Domain.Common;
using System.Linq.Expressions;

namespace ReimbursementPoC.Administration.Domain.Program.Specification
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