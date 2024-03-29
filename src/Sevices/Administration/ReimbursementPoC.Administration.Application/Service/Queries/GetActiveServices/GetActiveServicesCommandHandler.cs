﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Administration.Application.Common.Interfaces;
using ReimbursementPoC.Administration.Application.Common.Model;
using ReimbursementPoC.Administration.Application.Services.Queries.GetServiceById;
using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Service;

namespace ReimbursementPoC.Administration.Application.Services.Queries.GetServices
{
    public class GetActiveServicesCommandHandler
        : IRequestHandler<GetActiveServicesQuery, Result<PaginatedList<ServiceDto>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetActiveServicesCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedList<ServiceDto>>> Handle(GetActiveServicesQuery query, CancellationToken cancellationToken)
        {
            var root = (IQueryable<ServiceEntity>)_applicationDbContext.Services.Include(x => x.Program)
                .Where(x => !x.IsCanceled 
                && !x.Program.IsCanceled 
                && (x.Program.Period.StartDate >= DateTime.Now || x.Program.Period.EndDate >= DateTime.Now));

            // ToDo add specification
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
