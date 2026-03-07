using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using scheduling_management.Domain.Entities;

namespace scheduling_management.Infra.Data.Mappings;

public class ServiceMapping : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Services");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.DurationInMinutes)
            .IsRequired();

        builder.Property(s => s.PriceInCentavos)
            .IsRequired();

        builder.Property(s => s.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(s => s.CreatedAtUtc)
            .IsRequired();

        builder.Property(s => s.UpdatedAtUtc);

        builder.HasIndex(s => new { s.EstablishmentId, s.Name })
            .IsUnique();

        builder.HasOne(s => s.Establishment)
            .WithMany(e => e.Services)
            .HasForeignKey(s => s.EstablishmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.ProfessionalServices)
            .WithOne(ps => ps.Service)
            .HasForeignKey(ps => ps.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.Appointments)
            .WithOne(a => a.Service)
            .HasForeignKey(a => a.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

