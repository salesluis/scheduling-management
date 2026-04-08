using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.User;

public interface IUserUseCase
{
    Task<UserResponseDto> CreateAsync(CreateUserDto request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, UpdateUserDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default); 
}
