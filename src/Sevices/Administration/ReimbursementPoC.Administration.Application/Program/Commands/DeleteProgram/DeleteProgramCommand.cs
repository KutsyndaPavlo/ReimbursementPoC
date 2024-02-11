using MediatR;
using ReimbursementPoC.Administration.Domain.Common;

namespace ReimbursementPoC.Administration.Application.Program.Commands.DeleteProgram
{
    public class DeleteProgramCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }
}
