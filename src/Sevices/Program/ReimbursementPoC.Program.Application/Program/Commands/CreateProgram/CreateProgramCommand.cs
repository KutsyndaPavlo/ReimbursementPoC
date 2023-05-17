using MediatR;
using ReimbursementPoC.Program.Application.Program.Queries.GetProgramById;

namespace ReimbursementPoC.Program.Application.Program.Commands.CreateProgram
{
    public class CreateProgramCommand : IRequest<ProgramDto>
    {
        public string Name { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }
    }
}
