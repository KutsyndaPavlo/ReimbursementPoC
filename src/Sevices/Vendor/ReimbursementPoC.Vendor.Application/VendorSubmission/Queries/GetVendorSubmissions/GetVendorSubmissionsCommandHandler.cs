using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Vendor.Application.Common.Interfaces;
using ReimbursementPoC.Vendor.Application.Common.Model;
using ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById;
using ReimbursementPoC.Vendor.Domain.Vendor;

namespace ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendors
{
    public class GetVendorSubmissionsCommandHandler
        : IRequestHandler<GetVendorSubmissionsQuery, PaginatedList<VendorSubmissionDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetVendorSubmissionsCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<VendorSubmissionDto>> Handle(GetVendorSubmissionsQuery query, CancellationToken cancellationToken)
        {
            var root = (IQueryable<VendorSubmissionEntity>)_applicationDbContext.VendorSubmissions;

            var total = await root.LongCountAsync();

            var data = await root
               // .OrderBy(c => c.Name)
                .Skip(query.Offset)
                .Take(query.Limit)
                .ToListAsync();

            return new PaginatedList<VendorSubmissionDto>
            {
                Items = data.Select(x => _mapper.Map<VendorSubmissionDto>(x)),
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
