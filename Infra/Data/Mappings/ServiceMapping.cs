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

        builder.Property(s => s.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier");

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("Name")
            .HasColumnType("nvarchar");

        builder.Property(s => s.DurationInMinutes)
            .IsRequired()
            .HasColumnName("DurationInMinutes")
            .HasColumnType("int");

        builder.Property(s => s.PriceInReal)
            .IsRequired()
            .HasColumnName("PriceInReal")
            .HasColumnType("int");

        builder.Property(s => s.IsActive)
            .IsRequired()
            .HasDefaultValue(true)
            .HasColumnName("IsActive")
            .HasColumnType("bit");

        builder.Property(s => s.EstablishmentId)
            .HasColumnName("EstablishmentId")
            .HasColumnType("uniqueidentifier");

        builder.Property(s => s.CreatedAtUtc)
            .IsRequired()
            .HasColumnName("CreatedAtUtc")
            .HasColumnType("datetime2");

        builder.Property(s => s.UpdatedAtUtc)
            .HasColumnName("UpdatedAtUtc")
            .HasColumnType("datetime2");

        builder.HasIndex(s => new { s.EstablishmentId, s.Name })
            .IsUnique()
            .HasDatabaseName("IX_Services_EstablishmentId_Name");

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
