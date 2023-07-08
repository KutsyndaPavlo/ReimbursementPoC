using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Customer.Application.Common.Interfaces;
using ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomerById;
using ReimbursementPoC.Customer.Domain;
using ReimbursementPoC.Customer.Domain.CustomerSubmission.Specification;

namespace ReimbursementPoC.Customer.Application.Customer.Commands.DeactivateCustomer
{
    public class DeactivateCustomerSubmissionCommandHandler : IRequestHandler<DeactivateCustomerSubmissionCommand, CustomerSubmissionDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public DeactivateCustomerSubmissionCommandHandler(IApplicationDbContext applicationDbContext, 
                                              IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<CustomerSubmissionDto> Handle(DeactivateCustomerSubmissionCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.CustomerSubmissions.FirstOrDefaultAsync(new CustomerSubmissionByIdSpecification(command.Id).ToExpression());


            if (entity == null)
            {
                throw new CustomerSubmissionNotFoundException($"Customer with id {command.Id} doesn't exist.");
            }

            entity.DeActivate();

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<CustomerSubmissionDto>(entity);

            return dto;
        }
    }
}
