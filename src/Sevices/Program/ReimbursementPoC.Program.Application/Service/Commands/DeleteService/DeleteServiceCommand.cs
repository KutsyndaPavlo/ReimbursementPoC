using MediatR;

namespace ReimbursementPoC.Program.Application.Services.Commands.DeleteService
{
    public class DeleteServiceCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
