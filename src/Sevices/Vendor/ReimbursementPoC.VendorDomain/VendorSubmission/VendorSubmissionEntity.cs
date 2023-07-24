using ReimbursementPoC.Vendor.Domain.Common;
using ReimbursementPoC.Vendor.Domain.Product.Rules;
using ReimbursementPoC.Vendor.Domain.Vendor.Events;

namespace ReimbursementPoC.Vendor.Domain.Vendor
{
    public class VendorSubmissionEntity : BaseEntity, IAggregateRoot
    {
        private VendorSubmissionEntity()
        {
            // oly for EF
        }

        private VendorSubmissionEntity(Guid vendorId, Guid serviceId, string serviceFullName, string description) : base()
        {
            this.VendorId = vendorId;
            this.ServiceId = serviceId;

            this._domainEvents.Add(new VendorSubmissionCreatedEvent(this));
            ServiceFullName = serviceFullName;
            Description = description;
        }

        public Guid VendorId { get; private set; }

        public Guid ServiceId { get; private set; }

        public string ServiceFullName { get; private set; }

        public string Description { get; set; }

        public bool IsCanceled { get; private set; }

        public static VendorSubmissionEntity CreateNew(Guid vendorId, Guid serviceId, string serviceFullName, string description)
        {
            CheckRule(new VendorSubmissionNameMustBeUniqueRule(serviceId));

            return new VendorSubmissionEntity(vendorId, serviceId, serviceFullName, description);
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