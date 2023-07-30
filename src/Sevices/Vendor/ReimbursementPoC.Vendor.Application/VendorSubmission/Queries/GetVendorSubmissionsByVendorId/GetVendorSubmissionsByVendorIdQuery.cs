using MediatR;
using ReimbursementPoC.Vendor.Application.Common.Model;
using ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById;
using System.Runtime.Serialization;

namespace ReimbursementPoC.Vendor.Application.VendorSubmission.Queries.GetVendorSubmissionsByVendorId
{
    public class GetVendorSubmissionsByVendorIdQuery : IRequest<PaginatedList<VendorSubmissionDto>>
    {
        [DataMember]
        public int Limit { get; set; }


        [DataMember]
        public int Offset { get; set; }

        [DataMember]
        public Guid VendorId { get; set; }

        public GetVendorSubmissionsByVendorIdQuery()
        {

        }

        public GetVendorSubmissionsByVendorIdQuery(int offset, int limit, Guid vendorId)
        {
            Offset = offset;
            Limit = limit;
            VendorId = vendorId;
        }
    }
}
