using FluentValidation;
using ReimbursementPoC.Program.Application.Common.Interfaces;

namespace ReimbursementPoC.Program.Application.Program.Commands.CreateProgram
{
    public class CreateProgramCommandValidator : AbstractValidator<CreateProgramCommand>
    {
        public CreateProgramCommandValidator(IApplicationDbContext applicationDbContext)
        {
            RuleFor(v => v.Name)
                .NotEmpty();
        }
    }
}
