using MediatR;
using ReimbursementPoC.Administration.Application.Common.Model;
using ReimbursementPoC.Administration.Application.Services.Queries.GetServiceById;
using System.Runtime.Serialization;

namespace ReimbursementPoC.Administration.Application.Services.Queries.GetServices
{
    public class GetActiveServicesQuery : IRequest<PaginatedList<ServiceDto>>
    {
        [DataMember]
        public int Limit { get; set; }


        [DataMember]
        public int Offset { get; set; }

        public GetActiveServicesQuery()
        {

        }

        public GetActiveServicesQuery(int offset, int limit)
        {
            Offset = offset;
            Limit = limit;
        }
    }
}
