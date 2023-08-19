using ReimbursementPoC.Administration.Domain.Common;
using System.Linq.Expressions;

namespace ReimbursementPoC.Administration.Domain.Program.Specifications
{
    public class ProgramIsNotCanceledSpecification : Specification<ProgramEntity>
    {
        public override Expression<Func<ProgramEntity, bool>> ToExpression()
        {
            return (item) => !item.IsCanceled;
        }
    }
}
