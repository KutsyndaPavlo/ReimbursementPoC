using ReimbursementPoC.Vendor.Domain.Common;
using ReimbursementPoC.Vendor.Domain.Product.Rules;
using ReimbursementPoC.Vendor.Domain.Vendor.Events;
using ReimbursementPoC.Vendor.Domain.VendorSubmission.DomainServices;

namespace ReimbursementPoC.Vendor.Domain.Vendor
{
    public class VendorSubmissionEntity : BaseEntity, IAggregateRoot
    {
        private VendorSubmissionEntity()
        {
            // oly for EF
        }

        private VendorSubmissionEntity(Guid vendorId, string vendorName, Guid serviceId, string serviceFullName, string description) : base()
        {
            this.VendorId = vendorId;
            this.VendorName = vendorName;
            this.ServiceId = serviceId;
            ServiceFullName = serviceFullName;
            Description = description;

            this._domainEvents.Add(new VendorSubmissionCreatedEvent(this));
        }

        public Guid VendorId { get; private set; }

        public string VendorName { get; private set; }

        public Guid ServiceId { get; private set; }

        public string ServiceFullName { get; private set; }

        public string? Description { get; set; }

        public bool IsCanceled { get; private set; }

        public static VendorSubmissionEntity CreateNew(
            Guid vendorId, 
            string vendorName,
            Guid serviceId, 
            string serviceFullName, 
            string description,
            IVendorSubmissionService vendorSubmissionService)
        {
            CheckRule(new VendorSubmissionMustBeSinglePerServiceRule(serviceId, vendorId, vendorSubmissionService));

            return new VendorSubmissionEntity(vendorId, vendorName, serviceId, serviceFullName, description);
        }

        public void Cancel()
        {
            IsCanceled = true;
            this.LastModified = DateTime.UtcNow;
            this._domainEvents.Add(new VendorSubmissionCanceledEvent(this));
        }

        public bool CanBeDeleted()
        {
            return true;
            //return !productService.HistoricalProposals(this).Any();
        }
    }
}