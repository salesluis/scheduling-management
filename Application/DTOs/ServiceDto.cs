using System;

namespace scheduling_management.Application.DTOs;

public record CreateServiceDto(
    Guid EstablishmentId,
    string Name,
    int DurationInMinutes,
    int PriceInReal,
    string? Color = null,
    string? Description = null);

public record UpdateServiceDto(
    string Name,
    int DurationInMinutes,
    int PriceInReal,
    string? Color = null,
    string? Description = null);

public record ServiceResponseDto(
    Guid Id,
    Guid EstablishmentId,
    string Name,
    int DurationInMinutes,
    int PriceInReal,
    bool IsActive,
    string Color,
    string Description);
