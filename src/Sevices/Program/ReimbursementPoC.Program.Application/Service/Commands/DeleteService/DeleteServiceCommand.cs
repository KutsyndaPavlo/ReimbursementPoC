using MediatR;
using System.Runtime.Serialization;

namespace ReimbursementPoC.Program.Application.Services.Commands.DeleteService
{
    public class DeleteServiceCommand : IRequest<bool>
    {
        public DeleteServiceCommand(Guid id)
        {
            Id = id;
        }

        [DataMember]
        public Guid Id { get; set; }
    }
}
