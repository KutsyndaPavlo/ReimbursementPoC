using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReimbursementPoC.Program.Domain.Program.Enums;

namespace ReimbursementPoC.Program.Infrastructure.Persistence.Configurations
{
    public class StateTypeConfiguration : IEntityTypeConfiguration<StateType>
    {
        public void Configure(EntityTypeBuilder<StateType> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
                .HasColumnName("Id").ValueGeneratedNever();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("Name");
        }
    }
}
