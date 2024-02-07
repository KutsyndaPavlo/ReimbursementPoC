using MediatR;
using ReimbursementPoC.Administration.Application.Services.Queries.GetServiceById;
using ReimbursementPoC.Administration.Domain.Common;

namespace ReimbursementPoC.Administration.Application.Services.Commands.DeactivateService
{
    public class CancelServiceCommand : IRequest<Result<ServiceDto>>
    {
        public Guid Id { get; set; }
    }
}
