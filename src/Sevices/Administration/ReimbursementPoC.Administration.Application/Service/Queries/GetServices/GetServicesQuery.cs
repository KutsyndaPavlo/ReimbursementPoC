using MediatR;
using ReimbursementPoC.Administration.Application.Common.Model;
using ReimbursementPoC.Administration.Application.Services.Queries.GetServiceById;
using System.Runtime.Serialization;

namespace ReimbursementPoC.Administration.Application.Services.Queries.GetServices
{
    public class GetServicesQuery : IRequest<PaginatedList<ServiceDto>>
    {
        [DataMember]
        public int Limit { get; set; }


        [DataMember]
        public int Offset { get; set; }

        public GetServicesQuery()
        {

        }

        public GetServicesQuery(int offset, int limit)
        {
            Offset = offset;
            Limit = limit;
        }
    }
}
