﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Administration.Application.Common.Interfaces;
using ReimbursementPoC.Administration.Application.Common.Model;
using ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Product.Spefifications;
using ReimbursementPoC.Administration.Domain.Program;

namespace ReimbursementPoC.Administration.Application.Program.Queries.GetPrograms
{
    public class GetProgramsCommandHandler
        : IRequestHandler<GetProgramsQuery, Result<PaginatedList<ProgramDto>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetProgramsCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedList<ProgramDto>>> Handle(GetProgramsQuery query, CancellationToken cancellationToken)
        {
            var root = (IQueryable<ProgramEntity>)_applicationDbContext.Programs;

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                root = root.Where(new ProgramsNameContainsSpecification(query.Name).ToExpression());
            }

            var total = await root.LongCountAsync();

            root = root.Include(x => x.State).Include(x=>x.Services);

            if (query.Sort == "nameAsc")
            {
                root = root.OrderBy(c => c.Name);
            }
            else if (query.Sort == "nameDesc")
            {
                root = root.OrderByDescending(c => c.Name);
            }
            else if (query.Sort == "dateDesc")
            {
                root = root.OrderByDescending(c => c.Period.EndDate);
            }
            else
            {
                root = root.OrderBy(c => c.Period.EndDate);
            }

            var data = await root.Skip(query.Offset)
            .Take(query.Limit)
            .AsNoTracking()
            .ToListAsync();

            return Result<PaginatedList<ProgramDto>>.Success(new PaginatedList<ProgramDto>
            {
                Items = data.Select(x => _mapper.Map<ProgramDto>(x)),
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
