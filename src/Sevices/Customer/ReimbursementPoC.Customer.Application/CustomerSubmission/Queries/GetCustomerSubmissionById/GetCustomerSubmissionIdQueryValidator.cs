using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomerById
{
    public class GetCustomerSubmissionByIdQueryValidator : AbstractValidator<GetCustomerSubmissionByIdQuery>
    {
        public GetCustomerSubmissionByIdQueryValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty()
                .WithMessage("Id is required.");
        }
    }
}
