using ReimbursementPoC.Administration.Domain.Common;
using System.Linq.Expressions;

namespace ReimbursementPoC.Administration.Domain.Program.Specifications
{
    public class ProgramPeriodIntersectsSpecification : Specification<ProgramEntity>
    {
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;

        public ProgramPeriodIntersectsSpecification(DateTime startDate, DateTime endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
        }

        public override Expression<Func<ProgramEntity, bool>> ToExpression()
        {
            return (item) => (item.Period.EndDate >= _startDate || item.Period.StartDate >= _endDate);
        }
    }
}
