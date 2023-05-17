using MediatR;
using ReimbursementPoC.Program.Application.Program.Queries.GetProgramById;

namespace ReimbursementPoC.Program.Application.Program.Commands.UpdateProgram
{
    public class UpdateProgramCommand : IRequest<ProgramDto>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }

        public DateTime LastModified { get; set; }
    }
}
