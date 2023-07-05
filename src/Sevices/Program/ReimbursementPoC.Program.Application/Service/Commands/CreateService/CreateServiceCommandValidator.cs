using FluentValidation;
using ReimbursementPoC.Program.Application.Common.Interfaces;

namespace ReimbursementPoC.Program.Application.Services.Commands.CreateService
{
    public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
    {
        public CreateServiceCommandValidator(IApplicationDbContext applicationDbContext)
        {
            RuleFor(v => v.Name)
                .NotEmpty();
        }
    }
}
