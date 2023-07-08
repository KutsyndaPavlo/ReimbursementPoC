using ReimbursementPoC.Program.Application.Common.Interfaces;
using ReimbursementPoC.Program.Domain.Product.Spefifications;
using ReimbursementPoC.Program.Domain.Program;

namespace ReimbursementPoC.Program.Application.Program.DomainServices
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

        public bool IsUniqueName(string name)
        {
            return !_applicationDbContext.Programs.Any(new ProgramsNameEqualsSpecification(name).ToExpression());
        }
    }
}
