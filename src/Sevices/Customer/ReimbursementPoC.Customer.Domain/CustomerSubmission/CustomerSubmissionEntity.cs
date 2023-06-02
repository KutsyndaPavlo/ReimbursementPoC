using ReimbursementPoC.Customer.Domain.Common;
using ReimbursementPoC.Customer.Domain.Customer.Events;

namespace ReimbursementPoC.Customer.Domain.Customer
{
    public class CustomerSubmissionEntity : BaseEntity, IAggregateRoot
    {
        private CustomerSubmissionEntity()
        {

        }

        private CustomerSubmissionEntity(Guid customerId, Guid vendorSubmissionId) : base()
        {
            this.CustomerId = customerId;
            this.VendorSubmissionId = vendorSubmissionId;

            this._domainEvents.Add(new CustomerSubmissionCreatedEvent(this));
        }

        public Guid CustomerId { get; private set; }

        public Guid VendorSubmissionId { get; private set; }

        public bool IsActive { get; private set; }

        public static CustomerSubmissionEntity CreateNew(Guid customerId, Guid serviceId)
        {
            //CheckRule(new CustomerNameMustBeUniqueRule(programUniquenessChecker, name));

            return new CustomerSubmissionEntity(customerId, serviceId);
        }

        public void DeActivate()
        {
            IsActive = false;
            this.LastModified = DateTime.UtcNow;
            this._domainEvents.Add(new CustomerSubmissionDeactivatedEvent(this));
        }

        public bool CanBeDeleted()
        {
            return true;
            //return !productService.HistoricalProposals(this).Any();
        }
    }
}