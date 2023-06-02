using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Customer.Application.Common.Interfaces;
using ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomerById;
using ReimbursementPoC.Customer.Domain.Customer;
using ReimbursementPoC.Customer.Domain.CustomerSubmission.Specification;

namespace ReimbursementPoC.Customer.Application.Customer.Commands.CreateCustomer
{
    public class CreateCustomerSubmissionCommandHandler : IRequestHandler<CreateCustomerSubmissionCommand, CustomerSubmissionDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public CreateCustomerSubmissionCommandHandler(IApplicationDbContext applicationDbContext, 
                                                    IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<CustomerSubmissionDto> Handle(CreateCustomerSubmissionCommand command, CancellationToken cancellationToken)
        {
            var entity = CustomerSubmissionEntity.CreateNew(
                command.CustomerId,
                command.VendorSubmissionId);

            _applicationDbContext.CustomerSubmissions.Add(entity);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var result = await _applicationDbContext
                .CustomerSubmissions
                .FirstOrDefaultAsync(new CustomerSubmissionByIdSpecification(entity.Id).ToExpression());

            var dto = _mapper.Map<CustomerSubmissionDto>(result);

            return dto;
        }
    }
}
