using FluentValidation;

namespace ReimbursementPoC.Vendor.Application.VendorSubmission.Queries.GetVendorSubmissionsByVendorId
{
    internal class GetVendorSubmissionsByVendorIdQueryValidator : AbstractValidator<GetVendorSubmissionsByVendorIdQuery>
    {
        public GetVendorSubmissionsByVendorIdQueryValidator()
        {
            RuleFor(v => v.Offset)
              .GreaterThanOrEqualTo(0);

            RuleFor(v => v.Limit)
             .GreaterThan(0)
             .LessThanOrEqualTo(1000);
        }
    }
}