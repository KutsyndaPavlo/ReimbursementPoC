using FluentValidation;

namespace ReimbursementPoC.Administration.Application.Services.Commands.UpdateService
{
    public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
    {
        public UpdateServiceCommandValidator()
        {
            //RuleFor(v => v.Name)
            //    .NotEmpty();
        }
    }
}
