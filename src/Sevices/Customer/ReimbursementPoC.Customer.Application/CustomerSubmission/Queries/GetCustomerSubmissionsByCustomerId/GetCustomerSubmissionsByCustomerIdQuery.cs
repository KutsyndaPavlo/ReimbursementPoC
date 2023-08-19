using MediatR;
using ReimbursementPoC.Customer.Application.Common.Model;
using ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomerById;
using System.Runtime.Serialization;

namespace ReimbursementPoC.Customer.Application.CustomerSubmission.Queries.GetCustomerSubmissionsByCustomerId
{
    public class GetCustomerSubmissionsByCustomerIdQuery : IRequest<PaginatedList<CustomerSubmissionDto>>
    {
        [DataMember]
        public int Limit { get; set; }

        [DataMember]
        public int Offset { get; set; }

        [DataMember]
        public Guid CustomerId { get; set; }

        public GetCustomerSubmissionsByCustomerIdQuery()
        {

        }

        public GetCustomerSubmissionsByCustomerIdQuery(Guid customerId, int offset, int limit)
        {
            Offset = offset;
            Limit = limit;
            CustomerId = customerId;
        }
    }
}
