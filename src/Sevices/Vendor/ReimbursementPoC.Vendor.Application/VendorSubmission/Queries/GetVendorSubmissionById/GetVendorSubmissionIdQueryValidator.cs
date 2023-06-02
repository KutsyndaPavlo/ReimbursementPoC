using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById
{
    public class GetVendorSubmissionByIdQueryValidator : AbstractValidator<GetVendorSubmissionByIdQuery>
    {
        public GetVendorSubmissionByIdQueryValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty()
                .WithMessage("Id is required.");
        }
    }
}
