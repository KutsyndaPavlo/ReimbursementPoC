using MediatR;
using ReimbursementPoC.Program.Application.Services.Queries.GetServiceById;

namespace ReimbursementPoC.Program.Application.Services.Commands.DeactivateService
{
    public class DeactivateServiceCommand : IRequest<ServiceDto>
    {
        public Guid Id { get; set; }
    }
}
