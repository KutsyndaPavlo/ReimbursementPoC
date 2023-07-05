using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Customer.Application.Common.Interfaces;
using ReimbursementPoC.Customer.Domain;
using ReimbursementPoC.Customer.Domain.CustomerSubmission.Specification;

namespace ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomerById
{
    public class GetCustomerSubmissionByIdQueryHandler
        : IRequestHandler<GetCustomerSubmissionByIdQuery, CustomerSubmissionDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetCustomerSubmissionByIdQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<CustomerSubmissionDto> Handle(GetCustomerSubmissionByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext
                .CustomerSubmissions
                .FirstOrDefaultAsync(new CustomerSubmissionByIdSpecification(query.Id).ToExpression());

            if (entity == null)
            {
                throw new CustomerSubmissionNotFoundException($"Customer with id {query.Id} doesn't exist");
            }

            return _mapper.Map<CustomerSubmissionDto>(entity);
        }
    }
}
