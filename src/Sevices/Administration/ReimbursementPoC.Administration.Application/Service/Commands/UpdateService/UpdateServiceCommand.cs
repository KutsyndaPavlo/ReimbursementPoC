using MediatR;
using ReimbursementPoC.Administration.Application.Services.Queries.GetServiceById;
using ReimbursementPoC.Administration.Domain.Common;

namespace ReimbursementPoC.Administration.Application.Services.Commands.UpdateService
{
    public class UpdateServiceCommand : IRequest<Result<ServiceDto>>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime LastModified { get; set; }
    }
}
