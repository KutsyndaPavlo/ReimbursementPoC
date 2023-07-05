using MediatR;
using ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById;

namespace ReimbursementPoC.Vendor.Application.Vendor.Commands.DeactivateVendor
{
    public class DeactivateVendorSubmissionCommand : IRequest<VendorSubmissionDto>
    {
        public Guid Id { get; set; }
    }
}
