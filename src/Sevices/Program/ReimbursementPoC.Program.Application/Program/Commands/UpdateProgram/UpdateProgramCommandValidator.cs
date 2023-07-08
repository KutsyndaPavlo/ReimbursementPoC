using FluentValidation;

namespace ReimbursementPoC.Program.Application.Program.Commands.UpdateProgram
{
    public class UpdateProgramCommandValidator : AbstractValidator<UpdateProgramCommand>
    {
        public UpdateProgramCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty();
        }
    }
}
