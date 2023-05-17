using MediatR;
using ReimbursementPoC.Program.Application.Program.Queries.GetProgramById;

namespace ReimbursementPoC.Program.Application.Program.Commands.DeactivateProgram
{
    public class DeactivateProgramCommand : IRequest<ProgramDto>
    {
        public Guid Id { get; set; }

        public DateTime LastModified { get; set; }
    }
}
