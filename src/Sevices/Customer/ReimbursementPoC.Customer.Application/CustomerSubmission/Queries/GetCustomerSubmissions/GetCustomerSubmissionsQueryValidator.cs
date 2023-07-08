using FluentValidation;

namespace ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomers
{
    public class GetCustomerSubmissionsQueryValidator : AbstractValidator<GetCustomerSubmissionsQuery>
    {
        public GetCustomerSubmissionsQueryValidator()
        {
            RuleFor(v => v.Offset)
              .GreaterThanOrEqualTo(0);

            RuleFor(v => v.Limit)
             .GreaterThan(0)
             .LessThanOrEqualTo(1000);
        }
    }
}
