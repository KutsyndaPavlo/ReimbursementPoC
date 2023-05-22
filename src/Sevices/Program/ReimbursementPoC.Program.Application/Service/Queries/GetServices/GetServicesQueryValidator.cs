using FluentValidation;

namespace ReimbursementPoC.Program.Application.Services.Queries.GetServices
{
    public class GetServicesQueryValidator : AbstractValidator<GetServicesQuery>
    {
        public GetServicesQueryValidator()
        {
            RuleFor(v => v.Offset)
              .GreaterThanOrEqualTo(0);

            RuleFor(v => v.Limit)
             .GreaterThan(0)
             .LessThanOrEqualTo(1000);
        }
    }
}
