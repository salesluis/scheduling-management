using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using scheduling_management.Domain.Entities;

namespace scheduling_management.Infra.Data.Mappings;

public class ProfessionalServiceMapping : IEntityTypeConfiguration<ProfessionalService>
{
    public void Configure(EntityTypeBuilder<ProfessionalService> builder)
    {
        builder.ToTable("ProfessionalServices");

        builder.HasKey(ps => ps.Id);

        builder.Property(ps => ps.CreatedAtUtc)
            .IsRequired();

        builder.Property(ps => ps.UpdatedAtUtc);

        builder.HasIndex(ps => new { ps.EstablishmentId, ps.ProfessionalId, ps.ServiceId })
            .IsUnique();

        builder.HasOne(ps => ps.Professional)
            .WithMany(p => p.ProfessionalServices)
            .HasForeignKey(ps => ps.ProfessionalId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ps => ps.Service)
            .WithMany(s => s.ProfessionalServices)
            .HasForeignKey(ps => ps.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

