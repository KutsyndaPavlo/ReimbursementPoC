using FluentValidation;

namespace ReimbursementPoC.Administration.Application.Services.Queries.GetServiceById
{
    public class GetServiceByIdQueryValidator : AbstractValidator<GetServiceByIdQuery>
    {
        public GetServiceByIdQueryValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty()
                .WithMessage("Service Id is required.");
        }
    }
}
