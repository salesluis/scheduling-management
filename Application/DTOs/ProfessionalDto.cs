namespace scheduling_management.Application.DTOs;

public record CreateProfessionalDto(
    Guid EstablishmentId,
    Guid UserId,
    string DisplayName);

public record UpdateProfessionalDto(string DisplayName);

public record ProfessionalDto(
    Guid Id,
    Guid EstablishmentId,
    Guid UserId,
    string DisplayName,
    bool IsActive,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc);

/// <summary>Vínculo profissional-serviço (resposta de listagem por profissional).</summary>
public record ProfessionalServiceLinkDto(
    Guid Id,
    Guid EstablishmentId,
    Guid ProfessionalId,
    Guid ServiceId,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc);
