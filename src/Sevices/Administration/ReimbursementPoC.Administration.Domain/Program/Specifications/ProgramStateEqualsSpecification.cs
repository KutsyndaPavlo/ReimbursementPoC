using ReimbursementPoC.Administration.Domain.Common;
using System.Linq.Expressions;

namespace ReimbursementPoC.Administration.Domain.Program.Specifications
{
    public class ProgramStateEqualsSpecification : Specification<ProgramEntity>
    {
        private readonly int _stateId;

        public ProgramStateEqualsSpecification(int stateId)
        {
            _stateId = stateId;
        }

        public override Expression<Func<ProgramEntity, bool>> ToExpression()
        {
            return (item) => (item.State.Id == _stateId);
        }
    }
}
