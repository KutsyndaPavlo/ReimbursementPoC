using MediatR;
using System.Runtime.Serialization;

namespace ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById
{
    public class GetVendorSubmissionByIdQuery : IRequest<VendorSubmissionDto>
    {
        public GetVendorSubmissionByIdQuery(Guid id)
        {
            Id = id;
        }

        [DataMember]
        public Guid Id { get; set; }
    }
}
