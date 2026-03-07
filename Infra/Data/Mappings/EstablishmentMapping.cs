using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using scheduling_management.Domain.Entities;

namespace scheduling_management.Infra.Data.Mappings;

public class EstablishmentMapping : IEntityTypeConfiguration<Establishment>
{
    public void Configure(EntityTypeBuilder<Establishment> builder)
    {
        builder.ToTable("Establishments");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Slug)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(e => e.Slug)
            .IsUnique();

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.CreatedAtUtc)
            .IsRequired();

        builder.Property(e => e.UpdatedAtUtc);

        builder.HasMany(e => e.Services)
            .WithOne(s => s.Establishment)
            .HasForeignKey(s => s.EstablishmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Professionals)
            .WithOne(p => p.Establishment)
            .HasForeignKey(p => p.EstablishmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Clients)
            .WithOne(c => c.Establishment)
            .HasForeignKey(c => c.EstablishmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Appointments)
            .WithOne(a => a.Establishment)
            .HasForeignKey(a => a.EstablishmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

