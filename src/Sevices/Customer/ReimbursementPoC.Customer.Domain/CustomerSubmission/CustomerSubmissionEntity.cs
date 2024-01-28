using ReimbursementPoC.Customer.Domain.Common;
using ReimbursementPoC.Customer.Domain.Customer.Events;
using ReimbursementPoC.Customer.Domain.CustomerSubmission.DomainServices;
using ReimbursementPoC.Customer.Domain.Product.Rules;

namespace ReimbursementPoC.Customer.Domain.Customer
{
    public class CustomerSubmissionEntity : BaseEntity, IAggregateRoot
    {
        private CustomerSubmissionEntity()
        {
            // only for EF
        }

        private CustomerSubmissionEntity(
            Guid customerId,
            Guid vendorSubmissionId,
            string vendorName,
            string serviceFullName,
            string description) : base()
        {
            this.CustomerId = customerId;
            this.VendorSubmissionId = vendorSubmissionId;
            this.VendorName = vendorName;
            this.ServiceFullName = serviceFullName;
            this.Description = description;

            this._domainEvents.Add(new CustomerSubmissionCreatedEvent(this));
        }

        public Guid CustomerId { get; private set; }

        public Guid VendorSubmissionId { get; private set; }

        public bool IsCanceled { get; private set; }

        public string VendorName { get; set; }

        public string ServiceFullName { get; set; }

        public string? Description { get; set; }

        public static CustomerSubmissionEntity CreateNew(
            Guid customerId,
            Guid vendorSubmissionId,
            string vendorName,
            string serviceFullName,
            string description,
            ICustomerSubmissionService customerSubmissionService)
        {
            //ToDo add unique rule
            CheckRule(new CustomerSubmissionNameMustBeUniqueRule(customerSubmissionService, customerId, vendorSubmissionId));

            return new CustomerSubmissionEntity(customerId, vendorSubmissionId, vendorName, serviceFullName, description);
        }

        public void Cancel()
        {
            IsCanceled = true;
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