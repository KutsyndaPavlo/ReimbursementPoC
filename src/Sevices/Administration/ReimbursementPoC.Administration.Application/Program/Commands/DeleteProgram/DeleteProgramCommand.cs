using MediatR;

namespace ReimbursementPoC.Administration.Application.Program.Commands.DeleteProgram
{
    public class DeleteProgramCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
