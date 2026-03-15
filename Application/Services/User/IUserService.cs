using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.Services;

public interface IUserService
{
    Task<UserDto> CreateAsync(CreateUserDto request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, UpdateUserDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResponse<UserDto>> ListAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<UserDto>> SearchAsync(string? name, string? email, CancellationToken cancellationToken = default);
}
