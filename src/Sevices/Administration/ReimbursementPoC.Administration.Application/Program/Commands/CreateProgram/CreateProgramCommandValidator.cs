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
        }
    }
}
