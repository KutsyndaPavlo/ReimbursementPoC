using MediatR;
using ReimbursementPoC.Customer.Application.Common.Model;
using ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomerById;
using System.Runtime.Serialization;

namespace ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomers
{
    public class GetCustomerSubmissionsQuery : IRequest<PaginatedList<CustomerSubmissionDto>>
    {
        [DataMember]
        public int Limit { get; set; }


        [DataMember]
        public int Offset { get; set; }

        public GetCustomerSubmissionsQuery()
        {

        }

        public GetCustomerSubmissionsQuery(int offset, int limit)
        {
            Offset = offset;
            Limit = limit;
        }
    }
}
