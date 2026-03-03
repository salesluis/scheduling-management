using SchedulingManagement.Enums;

namespace SchedulingManagement.Entities;

public class Appointment : TenantEntity
{
    public Guid ProfessionalId { get; private set; }

    public Guid ServiceId { get; private set; }

    public Guid ClientId { get; private set; }

    public DateTime StartUtc { get; private set; }

    public DateTime EndUtc { get; private set; }

    public AppointmentStatus Status { get; private set; } = AppointmentStatus.Scheduled;

    public Professional Professional { get; private set; } = null!;

    public Service Service { get; private set; } = null!;

    public Client Client { get; private set; } = null!;
    
    public Appointment(
        Guid establishmentId,
        Guid professionalId,
        Guid serviceId,
        Guid clientId,
        DateTime startUtc,
        DateTime endUtc)
    {
        if (endUtc <= startUtc)
        {
            throw new ArgumentException("End time must be after start time.", nameof(endUtc));
        }

        EstablishmentId = establishmentId;
        ProfessionalId = professionalId;
        ServiceId = serviceId;
        ClientId = clientId;
        StartUtc = startUtc;
        EndUtc = endUtc;
    }

    public void Cancel()
    {
        if (Status == AppointmentStatus.Completed)
            throw new InvalidOperationException("Cannot cancel a completed appointment.");

        Status = AppointmentStatus.Cancelled;
    }

    public void Complete()
    {
        if (Status != AppointmentStatus.Scheduled)
            throw new InvalidOperationException("Only scheduled appointments can be completed.");

        Status = AppointmentStatus.Completed;
        Touch();
    }
}

