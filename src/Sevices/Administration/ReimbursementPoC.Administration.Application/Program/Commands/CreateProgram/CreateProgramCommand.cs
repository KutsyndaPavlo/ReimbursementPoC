using MediatR;
using ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById;

namespace ReimbursementPoC.Administration.Application.Program.Commands.CreateProgram
{
    public class CreateProgramCommand : IRequest<ProgramDto>
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public int StateId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
