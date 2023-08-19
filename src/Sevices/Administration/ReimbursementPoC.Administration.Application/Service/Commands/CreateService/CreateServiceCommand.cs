using MediatR;
using ReimbursementPoC.Administration.Application.Services.Queries.GetServiceById;

namespace ReimbursementPoC.Administration.Application.Services.Commands.CreateService
{
    public class CreateServiceCommand : IRequest<ServiceDto>
    {
        public Guid ProgramId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
