namespace scheduling_management.Application.DTOs;

public record CreateAppointmentDto(
    Guid EstablishmentId,
    Guid ProfessionalId,
    Guid ServiceId,
    Guid ClientId,
    DateOnly SchedulingDateOnly,
    TimeOnly StartHours,
    TimeOnly EndHours,
    int TotalClients);

public record UpdateAppointmentDto(
    DateOnly SchedulingDateOnly,
    TimeOnly StartHours,
    TimeOnly EndHours,
    int TotalClients);

public record AppointmentDto(
    Guid Id,
    Guid EstablishmentId,
    Guid ProfessionalId,
    Guid ServiceId,
    Guid ClientId,
    DateOnly SchedulingDateOnly,
    TimeOnly StartHours,
    TimeOnly EndHours,
    int Status,
    int TotalClients,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc);
