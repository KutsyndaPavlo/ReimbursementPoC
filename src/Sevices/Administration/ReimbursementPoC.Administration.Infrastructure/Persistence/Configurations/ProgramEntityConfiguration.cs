using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReimbursementPoC.Administration.Domain.Program;
using ReimbursementPoC.Administration.Domain.Program.Enums;
using ReimbursementPoC.Administration.Domain.ValueObjects;

namespace ReimbursementPoC.Administration.Infrastructure.Persistence.Configurations
{
    public class ProgramEntityConfiguration : IEntityTypeConfiguration<ProgramEntity>
    {
        public void Configure(EntityTypeBuilder<ProgramEntity> builder)
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

            builder.Property(t => t.Description)
                .HasColumnName("Description");

            builder
            .HasOne(pt => pt.State)
            .WithMany()
            .HasForeignKey("_stateId");

            builder.OwnsOne<Period>("Period", mv =>
                       {
                           mv.Property(p => p.StartDate).HasColumnName("StartDate");
                           mv.Property(p => p.EndDate).HasColumnName("EndDate");
                       });

            //builder.OwnsOne(i => i.Period, io =>
            //{
            //    // io.WithOwner();

            //    io.Property(cio => cio.StartDate).HasColumnName("StartDate");
            //    io.Property(cio => cio.EndDate).HasColumnName("EndDate");
            //});

            builder.Property(t => t.IsCanceled)
                .HasColumnName("IsCompleted")
                .IsRequired();

            builder.Property(t => t.LastModified)
                .HasColumnName("LastModified")
                .IsRequired();

            builder.Property(t => t.LastModifiedBy)
                .HasColumnName("LastModifiedBy")
                .IsRequired();

            var navigation = builder.Metadata.FindNavigation(nameof(ProgramEntity.Services));

            navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

            //builder.HasMany("_services")
            //    .WithOne("Program")
            //    .HasForeignKey("ProgramId")
            //    .IsRequired().OnDelete(DeleteBehavior.Cascade);


            builder.Ignore(e => e.DomainEvents);
        }
    }
}


