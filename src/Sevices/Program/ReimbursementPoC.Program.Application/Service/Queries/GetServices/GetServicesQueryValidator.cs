using FluentValidation;

namespace ReimbursementPoC.Program.Application.Services.Queries.GetServices
{
    public class GetServicesQueryValidator : AbstractValidator<GetServicesQuery>
    {
        public GetServicesQueryValidator()
        {
            //RuleFor(v => v.ProgramId).NotEmpty();
        }
    }
}
