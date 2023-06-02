using ReimbursementPoC.Vendor.Domain.Common;
using ReimbursementPoC.Vendor.Domain.Vendor.Events;

namespace ReimbursementPoC.Vendor.Domain.Vendor
{
    public class VendorSubmissionEntity : BaseEntity, IAggregateRoot
    {
        private VendorSubmissionEntity()
        {

        }

        private VendorSubmissionEntity(Guid vendorId, Guid serviceId) : base()
        {
            this.VendorId = vendorId;
            this.ServiceId = serviceId;

            this._domainEvents.Add(new VendorSubmissionCreatedEvent(this));
        }

        public Guid VendorId { get; private set; }

        public Guid ServiceId { get; private set; }

        public bool IsActive { get; private set; }

        public static VendorSubmissionEntity CreateNew(Guid vendorId, Guid serviceId)
        {
            //CheckRule(new VendorNameMustBeUniqueRule(programUniquenessChecker, name));

            return new VendorSubmissionEntity(vendorId, serviceId);
        }

        public void DeActivate()
        {
            IsActive = false;
            this.LastModified = DateTime.UtcNow;
            this._domainEvents.Add(new VendorSubmissionDeactivatedEvent(this));
        }

        public bool CanBeDeleted()
        {
            return true;
            //return !productService.HistoricalProposals(this).Any();
        }
    }
}