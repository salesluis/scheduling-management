namespace scheduling_management.Application.DTOs;

public record CreateUserDto(
    string Name,
    string Email,
    string? PhoneNumber = null);

public record UpdateUserDto(
    string Name,
    string Email,
    string? PhoneNumber = null);

public record UserDto(
    Guid Id,
    string Name,
    string Email,
    string? PhoneNumber,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc);
