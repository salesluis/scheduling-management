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

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.PhoneNumber)
            .HasMaxLength(12);

        builder.Property(c => c.CreatedAtUtc)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(c => c.UpdatedAtUtc)
            .HasColumnType("datetime");
        
        builder.HasIndex(c => new { c.EstablishmentId, c.UserId })
            .IsUnique();

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

