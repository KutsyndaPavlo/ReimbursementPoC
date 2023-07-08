using MediatR;

namespace ReimbursementPoC.Program.Application.Program.Commands.DeleteProgram
{
    public class DeleteProgramCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
