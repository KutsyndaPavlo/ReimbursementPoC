using MediatR;
using ReimbursementPoC.Program.Application.Services.Queries.GetServiceById;

namespace ReimbursementPoC.Program.Application.Services.Commands.CreateService
{
    public class CreateServiceCommand : IRequest<ServiceDto>
    {
        public Guid ProgramId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
