using MediatR;
using ReimbursementPoC.Administration.Application.Services.Queries.GetServiceById;

namespace ReimbursementPoC.Administration.Application.Services.Commands.DeactivateService
{
    public class DeactivateServiceCommand : IRequest<ServiceDto>
    {
        public Guid Id { get; set; }
    }
}
