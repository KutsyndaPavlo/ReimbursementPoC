using MediatR;
using System.Runtime.Serialization;

namespace ReimbursementPoC.Program.Application.Services.Queries.GetServiceById
{
    public class GetServiceByIdQuery : IRequest<ServiceDto>
    {
        public GetServiceByIdQuery(Guid id)
        {
            Id = id;
        }

        [DataMember]
        public Guid Id { get; set; }
    }
}
