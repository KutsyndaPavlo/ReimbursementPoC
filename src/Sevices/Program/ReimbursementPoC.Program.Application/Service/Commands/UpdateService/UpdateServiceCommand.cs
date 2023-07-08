using MediatR;
using ReimbursementPoC.Program.Application.Services.Queries.GetServiceById;

namespace ReimbursementPoC.Program.Application.Services.Commands.UpdateService
{
    public class UpdateServiceCommand : IRequest<ServiceDto>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime LastModified { get; set; }
    }
}
