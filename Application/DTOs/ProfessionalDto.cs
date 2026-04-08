using System;

namespace scheduling_management.Application.DTOs;

public record CreateProfessionalDto(
    Guid EstablishmentId,
    Guid UserId,
    string DisplayName);

public record UpdateProfessionalDto(string DisplayName);

public record ProfessionalResponseDto(
    Guid Id,
    Guid EstablishmentId,
    Guid UserId,
    string DisplayName,
    bool IsActive,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc);
