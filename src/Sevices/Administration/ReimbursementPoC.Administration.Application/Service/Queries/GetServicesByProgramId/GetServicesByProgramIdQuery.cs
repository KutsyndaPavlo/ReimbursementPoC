using MediatR;
using ReimbursementPoC.Administration.Application.Common.Model;
using ReimbursementPoC.Administration.Application.Services.Queries.GetServiceById;
using ReimbursementPoC.Administration.Domain.Common;
using System.Runtime.Serialization;

namespace ReimbursementPoC.Administration.Application.Service.Queries.GetServicesByProgramId
{
    public class GetServicesByProgramIdQuery : IRequest<Result<PaginatedList<ServiceDto>>>
    {
        [DataMember]
        public int Limit { get; set; }


        [DataMember]
        public int Offset { get; set; }

        [DataMember]
        public Guid ProgramId { get; set; }

        public GetServicesByProgramIdQuery()
        {

        }

        public GetServicesByProgramIdQuery(Guid programId, int offset, int limit)
        {
            ProgramId = programId;
            Offset = offset;
            Limit = limit;
        }
    }
}
