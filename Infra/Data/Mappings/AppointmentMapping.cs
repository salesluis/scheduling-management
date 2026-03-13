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

        builder.Property(a => a.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier");

        builder.Property(a => a.EstablishmentId)
            .HasColumnName("EstablishmentId")
            .HasColumnType("uniqueidentifier");

        builder.Property(a => a.ProfessionalId)
            .HasColumnName("ProfessionalId")
            .HasColumnType("uniqueidentifier");

        builder.Property(a => a.ClientId)
            .HasColumnName("ClientId")
            .HasColumnType("uniqueidentifier");

        builder.Property(a => a.ServiceId)
            .HasColumnName("ServiceId")
            .HasColumnType("uniqueidentifier");

        builder.Property(a => a.StartHours)
            .IsRequired()
            .HasColumnName("StartHours")
            .HasColumnType("time");

        builder.Property(a => a.EndHours)
            .IsRequired()
            .HasColumnName("EndHours")
            .HasColumnType("time");

        builder.Property(a => a.SchedulingDateOnly)
            .IsRequired()
            .HasColumnName("SchedulingDateOnly")
            .HasColumnType("date");

        builder.Property(a => a.TotalClients)
            .IsRequired()
            .HasDefaultValue(0)
            .HasColumnName("TotalClients")
            .HasColumnType("int");

        builder.Property(a => a.Status)
            .IsRequired()
            .HasColumnName("Status")
            .HasColumnType("smallint");

        builder.Property(a => a.CreatedAtUtc)
            .IsRequired()
            .HasColumnName("CreatedAtUtc")
            .HasColumnType("datetime2");

        builder.Property(a => a.UpdatedAtUtc)
            .HasColumnName("UpdatedAtUtc")
            .HasColumnType("datetime2");

        builder.HasIndex(a => new { a.EstablishmentId, a.ProfessionalId, a.StartHours })
            .HasDatabaseName("IX_Appointments_EstablishmentId_ProfessionalId_StartHours");

        builder.HasIndex(a => new { a.EstablishmentId, a.ClientId, a.StartHours })
            .HasDatabaseName("IX_Appointments_EstablishmentId_ClientId_StartHours");

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
