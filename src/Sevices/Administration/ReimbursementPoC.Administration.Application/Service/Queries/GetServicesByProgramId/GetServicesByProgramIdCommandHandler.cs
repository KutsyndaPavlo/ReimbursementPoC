using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Administration.Application.Common.Interfaces;
using ReimbursementPoC.Administration.Application.Common.Model;
using ReimbursementPoC.Administration.Application.Services.Queries.GetServiceById;
using ReimbursementPoC.Administration.Domain;
using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Program.Errors;
using ReimbursementPoC.Administration.Domain.Program.Specification;
using ReimbursementPoC.Administration.Domain.Service;
using ReimbursementPoC.Administration.Domain.Service.Specifications;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ReimbursementPoC.Administration.Application.Service.Queries.GetServicesByProgramId
{
    public class GetServicesByProgramIdCommandHandler
        : IRequestHandler<GetServicesByProgramIdQuery, Result<PaginatedList<ServiceDto>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetServicesByProgramIdCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedList<ServiceDto>>> Handle(GetServicesByProgramIdQuery query, CancellationToken cancellationToken)
        {
            //ToDo add specification
            var program = await _applicationDbContext.Programs
                .FirstOrDefaultAsync(new ProgramByIdSpecification(query.ProgramId).ToExpression());

            if (program == null)
            {
                return Result<PaginatedList<ServiceDto>>.Failure(ProgramErrors.NotFound(query.ProgramId));
            }

            var root = (IQueryable<ServiceEntity>)_applicationDbContext.Services.Include(x => x.Program)
                .Where(x => x.ProgramId == query.ProgramId);

            var total = await root.LongCountAsync();

            var data = await root
                .OrderBy(c => c.Name)
                .Skip(query.Offset)
                .Take(query.Limit)
                .AsNoTracking()
                .ToListAsync();


            return Result<PaginatedList<ServiceDto>>.Success(new PaginatedList<ServiceDto>
            {
                Items = data.Select(x => _mapper.Map<ServiceDto>(x)),
                Page = new Page
                {
                    Limit = query.Limit,
                    Offset = query.Offset,
                    Count = data.Count,
                    Total = total
                }
            });
        }
    }
}
