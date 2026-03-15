namespace scheduling_management.Application.DTOs;

/// <summary>Payload para criação de cliente.</summary>
public record CreateClientDto(
    Guid EstablishmentId,
    string Name,
    string? PhoneNumber = null,
    Guid? UserId = null);

/// <summary>Payload para atualização de cliente (id vem da rota).</summary>
public record UpdateClientDto(
    string Name,
    string? PhoneNumber = null);

/// <summary>Cliente retornado na API (get/list/search).</summary>
public record ResponseClientDto(
    Guid Id,
    Guid EstablishmentId,
    Guid? UserId,
    string Name,
    string? PhoneNumber,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc);
