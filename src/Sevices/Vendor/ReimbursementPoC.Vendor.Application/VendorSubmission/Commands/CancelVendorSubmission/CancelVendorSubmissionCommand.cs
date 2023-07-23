using MediatR;
using ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById;

namespace ReimbursementPoC.Vendor.Application.Vendor.Commands.DeactivateVendor
{
    public class CancelVendorSubmissionCommand : IRequest<VendorSubmissionDto>
    {
        public Guid VendorId { get; set; }
        public Guid SubmissionId { get; set; }
    }
}
