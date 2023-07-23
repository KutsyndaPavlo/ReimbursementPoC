using FluentValidation;
using ReimbursementPoC.Administration.Application.Common.Interfaces;

namespace ReimbursementPoC.Administration.Application.Program.Commands.CreateProgram
{
    public class CreateProgramCommandValidator : AbstractValidator<CreateProgramCommand>
    {
        public CreateProgramCommandValidator(IApplicationDbContext applicationDbContext)
        {
            RuleFor(v => v.Name)
                .NotEmpty();

            RuleFor(v => v.StateId)
                .NotEqual(default(int));

            RuleFor(v => v.StartDate)
                .NotEqual(default(DateTime));

            RuleFor(v => v.EndDate)
                .NotEqual(default(DateTime));

            RuleFor(v => v.StartDate)
                .LessThan(x => x.EndDate)
                .WithMessage("Start Date must be less than End Date");
        }
    }
}
