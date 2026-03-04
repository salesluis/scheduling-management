using scheduling_management.Domain.Abstractions;
using SchedulingManagement.Enums;

namespace scheduling_management.Domain.Entities;

public class Appointment : TenantEntity
{
    public Guid ProfessionalId { get; set; }
    public Guid ServiceId { get; set; }
    public Guid ClientId { get; set; }

    public DateOnly SchedulingDateOnly { get; set; }
    public TimeOnly StartHours { get; set; } 
    public TimeOnly EndHours { get; set; } 

    public AppointmentStatus Status { get; private set; } = AppointmentStatus.Scheduled;

    public Professional Professional { get; private set; } = null!;
    public Service Service { get; private set; } = null!;
    public Client Client { get; private set; }= null!;
    

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

