using scheduling_management.Domain.Entities;

namespace scheduling_management.Infrastructure.Repositories;

public interface IEstablishmentRepository
{
    Task<Establishment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Establishment?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResult<Establishment>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Establishment>> SearchAsync(string? name, bool? isActive, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Establishment> CreateAsync(Establishment entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Establishment entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Establishment entity, CancellationToken cancellationToken = default);
}
