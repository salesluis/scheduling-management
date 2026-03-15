using System;
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
    public EAppointmentStatus Status { get; private set; } = EAppointmentStatus.Scheduled;
    public int TotalClients { get; private set; } = 0;
    
    
    public Professional Professional { get; private set; } = null!;
    public Service Service { get; private set; } = null!;
    public Client Client { get; private set; }= null!;
    public Establishment Establishment { get; set; } = null!;

    public Appointment(Guid establishmentId, Guid professionalId, Guid serviceId, Guid clientId, DateOnly schedulingDateOnly, TimeOnly startHours, TimeOnly endHours, int totalClients)
    {
        EstablishmentId = establishmentId;
        ProfessionalId = professionalId;
        ServiceId = serviceId;
        ClientId = clientId;
        SchedulingDateOnly = schedulingDateOnly;
        StartHours = startHours;
        EndHours = endHours;
        TotalClients = totalClients;
    }

    public void Reschedule(DateOnly schedulingDateOnly, TimeOnly startHours, TimeOnly endHours)
    {
        SchedulingDateOnly = schedulingDateOnly;
        StartHours = startHours;
        EndHours = endHours;
        Touch();
    }

    public void SetTotalClients(int totalClients)
    {
        TotalClients = totalClients;
    }

    public void Cancel()
    {
        if (Status == EAppointmentStatus.Completed)
            throw new InvalidOperationException("Cannot cancel a completed appointment.");

        Status = EAppointmentStatus.Cancelled;
    }

    public void Complete()
    {
        if (Status != EAppointmentStatus.Scheduled)
            throw new InvalidOperationException("Only scheduled appointments can be completed.");

        Status = EAppointmentStatus.Completed;
        Touch();
    }
}

