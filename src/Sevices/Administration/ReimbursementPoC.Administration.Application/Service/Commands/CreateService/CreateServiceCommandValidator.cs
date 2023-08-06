using FluentValidation;
using ReimbursementPoC.Administration.Application.Common.Interfaces;

namespace ReimbursementPoC.Administration.Application.Services.Commands.CreateService
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
