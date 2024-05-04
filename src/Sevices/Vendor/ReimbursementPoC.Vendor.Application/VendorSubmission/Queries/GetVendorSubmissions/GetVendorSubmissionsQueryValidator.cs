using FluentValidation;

namespace ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendors
{
    public class GetVendorSubmissionsQueryValidator : AbstractValidator<GetVendorSubmissionsQuery>
    {
        public GetVendorSubmissionsQueryValidator()
        {
            RuleFor(v => v.Offset)
              .GreaterThanOrEqualTo(0);

            RuleFor(v => v.Limit)
             .GreaterThan(0)
             .LessThanOrEqualTo(1000);
        }
    }
}