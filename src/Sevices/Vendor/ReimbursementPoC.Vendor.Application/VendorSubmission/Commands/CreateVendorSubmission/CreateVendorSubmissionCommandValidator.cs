using FluentValidation;
using ReimbursementPoC.Vendor.Application.Common.Interfaces;

namespace ReimbursementPoC.Vendor.Application.Vendor.Commands.CreateVendor
{
    public class CreateVendorSubmissionCommandValidator : AbstractValidator<CreateVendorSubmissionCommand>
    {
        public CreateVendorSubmissionCommandValidator(IApplicationDbContext applicationDbContext)
        {
            //RuleFor(v => v.Name)
            //    .NotEmpty();
        }
    }
}
