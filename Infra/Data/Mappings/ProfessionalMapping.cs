using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using scheduling_management.Domain.Entities;

namespace scheduling_management.Infra.Data.Mappings;

public class ProfessionalMapping : IEntityTypeConfiguration<Professional>
{
    public void Configure(EntityTypeBuilder<Professional> builder)
    {
        builder.ToTable("Professionals");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.DisplayName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.CreatedAtUtc)
            .IsRequired();

        builder.Property(p => p.UpdatedAtUtc);

        builder.HasIndex(p => new { p.EstablishmentId, p.UserId })
            .IsUnique();

        builder.HasOne(p => p.Establishment)
            .WithMany(e => e.Professionals)
            .HasForeignKey(p => p.EstablishmentId)
            .OnDelete(DeleteBehavior.Restrict);
        

        builder.HasMany(p => p.ProfessionalServices)
            .WithOne(ps => ps.Professional)
            .HasForeignKey(ps => ps.ProfessionalId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Appointments)
            .WithOne(a => a.Professional)
            .HasForeignKey(a => a.ProfessionalId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

