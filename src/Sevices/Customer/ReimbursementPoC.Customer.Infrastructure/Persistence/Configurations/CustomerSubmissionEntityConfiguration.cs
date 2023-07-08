using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReimbursementPoC.Customer.Domain.Customer;

namespace ReimbursementPoC.Customer.Infrastructure.Persistence.Configurations
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerSubmissionEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerSubmissionEntity> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
                .HasColumnName("Id").ValueGeneratedNever();

            builder.Property(t => t.CustomerId)
                .HasColumnName("CustomerId").ValueGeneratedNever();

            builder.Property(t => t.VendorSubmissionId)
                .HasColumnName("VendorSubmissionId").ValueGeneratedNever();

            builder.Property(t => t.IsActive)
                .HasColumnName("IsActive").ValueGeneratedNever();

            builder.Ignore(e => e.DomainEvents);
        }
    }
}


