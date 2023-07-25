using FluentValidation;
using ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomers;

namespace ReimbursementPoC.Customer.Application.CustomerSubmission.Queries.GetCustomerSubmissionsByCustomerId
{
    public class GetCustomerSubmissionsByCustomerIdQueryValidator : AbstractValidator<GetCustomerSubmissionsQuery>
    {
        public GetCustomerSubmissionsByCustomerIdQueryValidator()
        {
            RuleFor(v => v.Offset)
              .GreaterThanOrEqualTo(0);

            RuleFor(v => v.Limit)
             .GreaterThan(0)
             .LessThanOrEqualTo(1000);
        }
    }
}
