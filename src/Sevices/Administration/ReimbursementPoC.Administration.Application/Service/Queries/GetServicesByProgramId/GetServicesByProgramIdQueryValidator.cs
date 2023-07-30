using FluentValidation;

namespace ReimbursementPoC.Administration.Application.Service.Queries.GetServicesByProgramId
{
    internal class GetServicesByProgramIdQueryValidator : AbstractValidator<GetServicesByProgramIdQuery>
    {
        public GetServicesByProgramIdQueryValidator()
        {
            RuleFor(v => v.ProgramId).NotEmpty();
        }
    }
}
