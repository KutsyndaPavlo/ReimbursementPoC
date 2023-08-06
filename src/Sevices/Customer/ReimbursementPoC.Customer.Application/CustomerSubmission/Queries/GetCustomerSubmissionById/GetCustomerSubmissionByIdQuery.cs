using MediatR;
using System.Runtime.Serialization;

namespace ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomerById
{
    public class GetCustomerSubmissionByIdQuery : IRequest<CustomerSubmissionDto>
    {
        public GetCustomerSubmissionByIdQuery(Guid id)
        {
            Id = id;
        }

        [DataMember]
        public Guid Id { get; set; }
    }
}
