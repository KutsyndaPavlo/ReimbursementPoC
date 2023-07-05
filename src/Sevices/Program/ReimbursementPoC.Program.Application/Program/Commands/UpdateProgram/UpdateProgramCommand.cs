using MediatR;
using ReimbursementPoC.Program.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Program.Application.Services.Queries.GetServiceById;

namespace ReimbursementPoC.Program.Application.Program.Commands.UpdateProgram
{
    public class UpdateProgramCommand : IRequest<ProgramDto>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int StateId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime LastModified { get; set; }
    }
}
