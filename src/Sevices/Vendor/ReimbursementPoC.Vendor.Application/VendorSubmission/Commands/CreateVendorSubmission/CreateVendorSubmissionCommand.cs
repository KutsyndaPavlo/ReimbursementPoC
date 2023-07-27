using MediatR;
using ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById;

namespace ReimbursementPoC.Vendor.Application.Vendor.Commands.CreateVendor
{
    public class CreateVendorSubmissionCommand : IRequest<VendorSubmissionDto>
    {
        public Guid VendorId { get; set; }

        public string VendorName { get; set; }

        public Guid ServiceId { get; set; }

        public string ServiceFullName { get; set; }

        public string Description { get; set; }
    }
}
