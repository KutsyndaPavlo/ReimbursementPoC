using MediatR;
using ReimbursementPoC.Program.Application.Common.Model;
using ReimbursementPoC.Program.Application.Services.Queries.GetServiceById;
using System.Runtime.Serialization;

namespace ReimbursementPoC.Program.Application.Services.Queries.GetServices
{
    public class GetServicesQuery : IRequest<PaginatedList<ServiceDto>>
    {

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Limit { get; set; }


        [DataMember]
        public int Offset { get; set; }

        public GetServicesQuery()
        {

        }

        public GetServicesQuery(string name, int offset, int limit)
        {
            Name = name;
            Offset = offset;
            Limit = limit;
        }
    }
}
