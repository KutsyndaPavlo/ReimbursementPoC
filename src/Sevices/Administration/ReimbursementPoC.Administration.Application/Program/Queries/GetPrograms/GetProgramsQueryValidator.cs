using FluentValidation;

namespace ReimbursementPoC.Administration.Application.Program.Queries.GetPrograms
{
    public class GetProgramsQueryValidator : AbstractValidator<GetProgramsQuery>
    {
        public GetProgramsQueryValidator()
        {
            RuleFor(v => v.Offset)
              .GreaterThanOrEqualTo(0);

            RuleFor(v => v.Limit)
             .GreaterThan(0)
             .LessThanOrEqualTo(1000);
        }
    }
}
