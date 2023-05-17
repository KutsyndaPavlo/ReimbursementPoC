using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReimbursementPoC.Program.Domain.Program;

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

            //builder.Property(t => t.IsCompleted)
            //    .HasColumnName("IsCompleted")
            //    .IsRequired();

            //builder.Property(t => t.Gender)
            //    .HasColumnName("Gender")
            //    .HasColumnType("int")
            //    .IsRequired();

            builder.Ignore(e => e.DomainEvents);
        }
    }
}


