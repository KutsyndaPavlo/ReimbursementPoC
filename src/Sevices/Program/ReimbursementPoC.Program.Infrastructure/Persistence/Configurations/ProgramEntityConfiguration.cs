using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReimbursementPoC.Program.Domain.Program;
using ReimbursementPoC.Program.Domain.ValueObjects;

namespace ReimbursementPoC.Program.Infrastructure.Persistence.Configurations
{
    public class ProgramEntityConfiguration : IEntityTypeConfiguration<ProgramEntity>
    {
        public void Configure(EntityTypeBuilder<ProgramEntity> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
                .HasColumnName("Id");

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("Name");

            builder.Property(t => t.Description)
                .HasColumnName("Description");

            //builder.HasOne(p => p.State)
            //.WithMany()

            builder.HasOne(x => x.State).WithMany();

            builder.OwnsOne<Period>("Period", mv =>
                       {
                           mv.Property(p => p.StartDate).HasColumnName("StartDate");
                           mv.Property(p => p.EndDate).HasColumnName("EndDate");
                       });

            builder.Property(t => t.IsActive)
                .HasColumnName("IsCompleted")
                .IsRequired();

            builder.Property(t => t.LastModified)
                .HasColumnName("LastModified")
                .IsRequired();

            builder.Property(t => t.LastModifiedBy)
                .HasColumnName("LastModifiedBy")
                .IsRequired();

            builder.Ignore(e => e.DomainEvents);
        }
    }
}


