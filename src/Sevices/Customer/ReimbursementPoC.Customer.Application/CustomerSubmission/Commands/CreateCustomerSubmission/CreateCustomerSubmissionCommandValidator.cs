using FluentValidation;
using ReimbursementPoC.Customer.Application.Common.Interfaces;

namespace ReimbursementPoC.Customer.Application.Customer.Commands.CreateCustomer
{
    public class CreateCustomerSubmissionCommandValidator : AbstractValidator<CreateCustomerSubmissionCommand>
    {
        public CreateCustomerSubmissionCommandValidator(IApplicationDbContext applicationDbContext)
        {
            //RuleFor(v => v.Name)
            //    .NotEmpty();
        }
    }
}
