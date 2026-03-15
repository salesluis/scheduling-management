namespace scheduling_management.Application.DTOs;

public record CreateEstablishmentDto(string Name);

public record UpdateEstablishmentDto(string Name);

public record RespondeEstablishmentDto(
    Guid Id,
    string Name,
    string Slug,
    bool IsActive,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc);
