using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using scheduling_management.Domain.Entities;

namespace scheduling_management.Infra.Data.Mappings;

public class AppointmentMapping : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.StartHours)
            .IsRequired();

        builder.Property(a => a.EndHours)
            .IsRequired();

        builder.Property(a => a.SchedulingDateOnly)
            .IsRequired();
        
        builder.Property(a => a.Status)
            .IsRequired();

        builder.Property(a => a.CreatedAtUtc)
            .IsRequired();

        builder.Property(a => a.UpdatedAtUtc);

        builder.HasIndex(a => new { a.EstablishmentId, a.ProfessionalId, a.StartHours });
        builder.HasIndex(a => new { a.EstablishmentId, a.ClientId, a.StartHours });

        builder.HasOne(a => a.Professional)
            .WithMany(e => e.Appointments)
            .HasForeignKey(a => a.ProfessionalId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(a => a.Client)
            .WithMany(c => c.Appointments)
            .HasForeignKey(a => a.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Service)
            .WithMany(s => s.Appointments)
            .HasForeignKey(a => a.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

