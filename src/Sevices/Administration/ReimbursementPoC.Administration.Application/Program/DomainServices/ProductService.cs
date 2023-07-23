using ReimbursementPoC.Administration.Application.Common.Interfaces;
using ReimbursementPoC.Administration.Domain.Program;
using ReimbursementPoC.Administration.Domain.Program.Specifications;

namespace ReimbursementPoC.Administration.Application.Program.DomainServices
{
    public class ProgramService : IProgramService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ProgramService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        //public IEnumerable<ProposalEntity> LatestProposals(ProgramEntity Program)
        //{
        //    return _applicationDbContext.Proposals
        //         .Where(new ActiveProposalsForProgramSpecification(Program.Id).ToExpression())
        //         .Join(_applicationDbContext.Sellers,
        //            pr => pr.SellerId,
        //            sel => sel.Id,
        //            (pr, sel) => new { pr, sel })
        //         .Where(x => x.sel.IsActive)
        //         .Select(x => x.pr)
        //         .GroupBy(x => x.SellerId)
        //         .Select(x => x.First(y => y.Date == x.Max(x => x.Date) && y.IsActive))
        //         .ToList()
        //         .Where(x => x != null);
        //}

        //public IEnumerable<ProposalEntity> HistoricalProposals(ProgramEntity Program, int offset = 0, int limit = 1)
        //{
        //    var filterExpression = new ActiveProposalsForProgramSpecification(Program.Id).ToExpression();

        //    return _applicationDbContext.Proposals
        //        .Where(filterExpression)
        //        .OrderBy(x => x.Date)
        //        .Skip(offset)
        //        .Take(limit);
        //}

        public bool IsSinglePerStatePerPeriod(
            int stateId,
            DateTime startDate,
            DateTime endDate)
        {
            var specifications = new ProgramIsNotCanceledSpecification()
                            .And(new ProgramStateEqualsSpecification(stateId))
                            .And(new ProgramPeriodIntersectsSpecification(startDate, endDate));

            return !_applicationDbContext.Programs.Any(specifications.ToExpression());
        }
    }
}
