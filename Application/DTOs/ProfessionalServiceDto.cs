namespace scheduling_management.Application.DTOs;

public record CreateProfessionalServiceDto(
    Guid EstablishmentId,
    Guid ProfessionalId,
    Guid ServiceId);

public record UpdateProfessionalServiceDto(
    Guid ProfessionalId,
    Guid ServiceId);

public record ProfessionalServiceDto(
    Guid Id,
    Guid EstablishmentId,
    Guid ProfessionalId,
    Guid ServiceId,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc);
