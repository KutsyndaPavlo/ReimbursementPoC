using FluentValidation;

namespace ReimbursementPoC.Administration.Application.Services.Queries.GetServices
{
    public class GetServicesQueryValidator : AbstractValidator<GetServicesQuery>
    {
        public GetServicesQueryValidator()
        {
            //RuleFor(v => v.ProgramId).NotEmpty();
        }
    }
}
