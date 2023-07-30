using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReimbursementPoC.Vendor.Domain.Vendor;

namespace ReimbursementPoC.Vendor.Infrastructure.Persistence.Configurations
{
    public class VendorEntityConfiguration : IEntityTypeConfiguration<VendorSubmissionEntity>
    {
        public void Configure(EntityTypeBuilder<VendorSubmissionEntity> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
                .HasColumnName("Id").ValueGeneratedNever();

            builder.Property(t => t.VendorId)
                .HasColumnName("VendorId").ValueGeneratedNever();

            builder.Property(t => t.ServiceId)
                .HasColumnName("ServiceId").ValueGeneratedNever();

            builder.Property(t => t.IsCanceled)
                .HasColumnName("IsActive").ValueGeneratedNever();

            builder.Ignore(e => e.DomainEvents);
        }
    }
}


