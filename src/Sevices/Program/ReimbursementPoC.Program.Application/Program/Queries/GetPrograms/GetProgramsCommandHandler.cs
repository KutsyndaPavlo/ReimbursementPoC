using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Program.Application.Common.Interfaces;
using ReimbursementPoC.Program.Application.Common.Model;
using ReimbursementPoC.Program.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Program.Domain.Product.Spefifications;
using ReimbursementPoC.Program.Domain.Program;

namespace ReimbursementPoC.Program.Application.Program.Queries.GetPrograms
{
    public class GetProgramsCommandHandler
        : IRequestHandler<GetProgramsQuery, PaginatedList<ProgramDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetProgramsCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ProgramDto>> Handle(GetProgramsQuery query, CancellationToken cancellationToken)
        {
            var root = (IQueryable<ProgramEntity>)_applicationDbContext.Programs;

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                root = root.Where(new ProgramsNameEqualsSpecification(query.Name).ToExpression());
            }

            var total = await root.LongCountAsync();

            var data = await root
                .Include(x => x.State)
                .OrderBy(c => c.Name)
                .Skip(query.Offset)
                .Take(query.Limit)
                .ToListAsync();

            return new PaginatedList<ProgramDto>
            {
                Items = data.Select(x => _mapper.Map<ProgramDto>(x)),
                Page = new Page
                {
                    Limit = query.Limit,
                    Offset = query.Offset,
                    Count = data.Count,
                    Total = total
                }
            };
        }
    }
}
