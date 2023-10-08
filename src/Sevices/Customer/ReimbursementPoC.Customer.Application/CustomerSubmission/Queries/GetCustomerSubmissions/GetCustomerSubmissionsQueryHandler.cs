using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Customer.Application.Common.Interfaces;
using ReimbursementPoC.Customer.Application.Common.Model;
using ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomerById;
using ReimbursementPoC.Customer.Domain.Customer;

namespace ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomers
{
    public class GetCustomerSubmissionsQueryHandler
        : IRequestHandler<GetCustomerSubmissionsQuery, PaginatedList<CustomerSubmissionDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetCustomerSubmissionsQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CustomerSubmissionDto>> Handle(GetCustomerSubmissionsQuery query, CancellationToken cancellationToken)
        {
            var root = (IQueryable<CustomerSubmissionEntity>)_applicationDbContext.CustomerSubmissions;

            var total = await root.LongCountAsync();

            var data = await root
               // .OrderBy(c => c.Name)
                .Skip(query.Offset)
                .Take(query.Limit)
                .ToListAsync();

            return new PaginatedList<CustomerSubmissionDto>
            {
                Items = data.Select(x => _mapper.Map<CustomerSubmissionDto>(x)),
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
