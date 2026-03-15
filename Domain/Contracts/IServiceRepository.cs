using scheduling_management.Domain.Entities;

namespace scheduling_management.Infrastructure.Repositories;

public interface IServiceRepository
{
    Task<Service?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Service?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResult<Service>> GetPagedAsync(int page, int pageSize, Guid? establishmentId = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Service>> SearchAsync(Guid? establishmentId, string? name, bool? isActive, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Service> CreateAsync(Service entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Service entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Service entity, CancellationToken cancellationToken = default);
}
