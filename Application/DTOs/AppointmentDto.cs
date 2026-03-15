using SchedulingManagement.Enums;

namespace scheduling_management.Application.DTOs;

public record CreateAppointmentDto(
    Guid EstablishmentId,
    Guid ProfessionalId,
    Guid ServiceId,
    Guid ClientId,
    DateOnly SchedulingDateOnly,
    TimeOnly StartHours,
    TimeOnly EndHours);

public record UpdateAppointmentDto(
    Guid Id,
    DateOnly SchedulingDateOnly,
    TimeOnly StartHours,
    TimeOnly EndHours);

public record ResponseAppointmentDto(
    Guid Id,
    Guid EstablishmentId,
    Guid ProfessionalId,
    Guid ServiceId,
    Guid ClientId,
    DateOnly SchedulingDateOnly,
    TimeOnly StartHours,
    TimeOnly EndHours,
    EAppointmentStatus Status);
