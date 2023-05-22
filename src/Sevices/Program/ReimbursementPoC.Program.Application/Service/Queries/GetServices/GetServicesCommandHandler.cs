using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Program.Application.Common.Interfaces;
using ReimbursementPoC.Program.Application.Common.Model;
using ReimbursementPoC.Program.Application.Services.Queries.GetServiceById;
using ReimbursementPoC.Program.Domain.Product.Spefifications;
using ReimbursementPoC.Program.Domain.Program;

namespace ReimbursementPoC.Program.Application.Services.Queries.GetServices
{
    public class GetServicesCommandHandler
        : IRequestHandler<GetServicesQuery, PaginatedList<ServiceDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetServicesCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ServiceDto>> Handle(GetServicesQuery query, CancellationToken cancellationToken)
        {
            var root = (IQueryable<ProgramEntity>)_applicationDbContext.Programs;

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                root = root.Where(new ProgramsNameEqualsSpecification(query.Name).ToExpression());
            }

            var total = await root.LongCountAsync();

            var data = await root.Include(x => x.State)
                .OrderBy(c => c.Name)
                .Skip(query.Offset)
                .Take(query.Limit)
                .ToListAsync();

            return new PaginatedList<ServiceDto>
            {
                Items = data.Select(x => _mapper.Map<ServiceDto>(x)),
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
