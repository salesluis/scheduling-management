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

        builder.Property(p => p.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier");

        builder.Property(p => p.DisplayName)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("DisplayName")
            .HasColumnType("nvarchar");

        builder.Property(p => p.IsActive)
            .IsRequired()
            .HasDefaultValue(true)
            .HasColumnName("IsActive")
            .HasColumnType("bit");

        builder.Property(p => p.EstablishmentId)
            .HasColumnName("EstablishmentId")
            .HasColumnType("uniqueidentifier");

        builder.Property(p => p.UserId)
            .HasColumnName("UserId")
            .HasColumnType("uniqueidentifier");

        builder.Property(p => p.CreatedAtUtc)
            .IsRequired()
            .HasColumnName("CreatedAtUtc")
            .HasColumnType("datetime2");
        
        builder.Property(a => a.TotalClients)
            .IsRequired()
            .HasDefaultValue(0)
            .HasColumnName("TotalClients")
            .HasColumnType("int");

        builder.Property(p => p.UpdatedAtUtc)
            .HasColumnName("UpdatedAtUtc")
            .HasColumnType("datetime2");

        builder.HasIndex(p => new { p.EstablishmentId, p.UserId })
            .IsUnique()
            .HasDatabaseName("IX_Professionals_EstablishmentId_UserId");

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
