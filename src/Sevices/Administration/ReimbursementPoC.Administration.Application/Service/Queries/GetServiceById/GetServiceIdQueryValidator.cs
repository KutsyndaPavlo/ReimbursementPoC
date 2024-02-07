using FluentValidation;

namespace ReimbursementPoC.Administration.Application.Services.Queries.GetServiceById
{
    public class GetServiceByIdQueryValidator : AbstractValidator<GetServiceByIdQuery>
    {
        public GetServiceByIdQueryValidator()
        {
            RuleFor(v => v.ServiceId)
                .NotEmpty()
                .WithMessage("ServiceId is required.");
        }
    }
}
