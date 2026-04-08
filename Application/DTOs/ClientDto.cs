using System;

namespace scheduling_management.Application.DTOs;

/// <summary>Payload para criação de cliente.</summary>
public record CreateClientDto(
    Guid EstablishmentId,
    string Name,
    string PhoneNumber);

/// <summary>Payload para atualização de cliente (id vem da rota).</summary>
public record UpdateClientDto(
    string Name,
    string PhoneNumber);

public record ClienResponsetDto(
    Guid Id,
    Guid EstablishmentId,
    Guid? UserId,
    string Name,
    string PhoneNumber);
