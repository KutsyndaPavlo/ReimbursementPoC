using FluentValidation;

namespace ReimbursementPoC.Administration.Application.Services.Queries.GetServices
{
    public class GetActiveServicesQueryValidator : AbstractValidator<GetActiveServicesQuery>
    {
        public GetActiveServicesQueryValidator()
        {
        }
    }
}
