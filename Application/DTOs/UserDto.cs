using System;

namespace scheduling_management.Application.DTOs;

public record CreateUserDto(
    string Name,
    string Email,
    string? PhoneNumber = null);

public record UpdateUserDto(
    string Name,
    string Email,
    string? PhoneNumber = null);

public record UserResponseDto(
    Guid Id,
    string Name,
    string Email);
