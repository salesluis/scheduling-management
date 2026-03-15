using scheduling_management.Domain.Entities;

namespace scheduling_management.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResult<User>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<User>> SearchAsync(string? name, string? email, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User> CreateAsync(User entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(User entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(User entity, CancellationToken cancellationToken = default);
}
