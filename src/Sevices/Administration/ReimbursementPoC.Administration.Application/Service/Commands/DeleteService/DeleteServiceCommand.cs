using MediatR;
using ReimbursementPoC.Administration.Domain.Common;
using System.Runtime.Serialization;

namespace ReimbursementPoC.Administration.Application.Services.Commands.DeleteService
{
    public class DeleteServiceCommand : IRequest<Result<bool>>
    {
        public DeleteServiceCommand(Guid id)
        {
            Id = id;
        }

        [DataMember]
        public Guid Id { get; set; }
    }
}
