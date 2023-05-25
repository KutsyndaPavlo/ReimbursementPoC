﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReimbursementPoC.Program.Domain.Program;
using ReimbursementPoC.Program.Domain.Program.Enums;
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

            builder.Property(t => t.IsActive)
                .HasColumnName("IsCompleted")
                .IsRequired();

            builder.Property(t => t.LastModified)
                .HasColumnName("LastModified")
                .IsRequired();

            builder.Property(t => t.LastModifiedBy)
                .HasColumnName("LastModifiedBy")
                .IsRequired();

            builder.HasMany("_services")
                .WithOne("Program")
                .HasForeignKey("ProgramId")
                .IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(e => e.DomainEvents);
        }
    }
}


