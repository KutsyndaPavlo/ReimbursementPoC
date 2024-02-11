using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById
{
    public class GetProgramByIdQueryValidator : AbstractValidator<GetProgramByIdQuery>
    {
        public GetProgramByIdQueryValidator()
        {
            //RuleFor(v => v.Id)
            //    .NotEmpty()
            //    .WithMessage("Id is required.");
        }
    }
}
