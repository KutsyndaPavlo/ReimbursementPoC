using MediatR;
using ReimbursementPoC.Vendor.Application.Common.Model;
using ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById;
using System.Runtime.Serialization;

namespace ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendors
{
    public class GetVendorSubmissionsQuery : IRequest<PaginatedList<VendorSubmissionDto>>
    {
        [DataMember]
        public int Limit { get; set; }


        [DataMember]
        public int Offset { get; set; }

        public GetVendorSubmissionsQuery()
        {

        }

        public GetVendorSubmissionsQuery(int offset, int limit)
        {
            Offset = offset;
            Limit = limit;
        }
    }
}
