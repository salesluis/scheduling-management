using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using scheduling_management.Domain.Entities;

namespace scheduling_management.Infra.Data.Mappings;

public class ClientMapping : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier");

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("Name")
            .HasColumnType("nvarchar");

        builder.Property(c => c.PhoneNumber)
            .HasMaxLength(12)
            .HasColumnName("PhoneNumber")
            .HasColumnType("nvarchar");

        builder.Property(c => c.EstablishmentId)
            .HasColumnName("EstablishmentId")
            .HasColumnType("uniqueidentifier");

        builder.Property(c => c.UserId)
            .HasColumnName("UserId")
            .HasColumnType("uniqueidentifier");

        builder.Property(c => c.CreatedAtUtc)
            .IsRequired()
            .HasColumnName("CreatedAtUtc")
            .HasColumnType("datetime2");

        builder.Property(c => c.UpdatedAtUtc)
            .HasColumnName("UpdatedAtUtc")
            .HasColumnType("datetime2");

        builder.HasIndex(c => new { c.EstablishmentId, c.UserId })
            .IsUnique()
            .HasDatabaseName("IX_Clients_EstablishmentId_UserId");

        builder.HasOne(c => c.Establishment)
            .WithMany(e => e.Clients)
            .HasForeignKey(c => c.EstablishmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Appointments)
            .WithOne(a => a.Client)
            .HasForeignKey(a => a.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
