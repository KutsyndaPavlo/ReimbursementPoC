using MediatR;

namespace ReimbursementPoC.Vendor.Application.Vendor.Commands.DeleteVendor
{
    public class DeleteVendorSubmissionCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
