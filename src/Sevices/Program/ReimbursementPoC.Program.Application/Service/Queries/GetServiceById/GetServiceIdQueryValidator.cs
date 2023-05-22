using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReimbursementPoC.Program.Application.Services.Queries.GetServiceById
{
    public class GetProgramByIdQueryValidator : AbstractValidator<GetServiceByIdQuery>
    {
        public GetProgramByIdQueryValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty()
                .WithMessage("Id is required.");
        }
    }
}
