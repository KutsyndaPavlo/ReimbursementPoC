using FluentValidation;

namespace ReimbursementPoC.Program.Application.Services.Commands.UpdateService
{
    public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
    {
        public UpdateServiceCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty();
        }
    }
}
