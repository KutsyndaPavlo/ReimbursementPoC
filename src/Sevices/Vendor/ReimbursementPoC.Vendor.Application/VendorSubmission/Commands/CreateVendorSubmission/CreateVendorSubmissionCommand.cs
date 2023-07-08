using MediatR;
using ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById;

namespace ReimbursementPoC.Vendor.Application.Vendor.Commands.CreateVendor
{
    public class CreateVendorSubmissionCommand : IRequest<VendorSubmissionDto>
    {
        public Guid VendorId { get; private set; }

        public Guid ServiceId { get; private set; }
    }
}
