using MediatR;
using ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Administration.Domain.Common;

namespace ReimbursementPoC.Administration.Application.Program.Commands.DeactivateProgram
{
    public class CancelProgramCommand : IRequest<Result<ProgramDto>>
    {
        public Guid Id { get; set; }
    }
}
