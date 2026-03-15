using scheduling_management.Domain.Entities;

namespace scheduling_management.Infrastructure.Repositories;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Client?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Client?> GetByIdWithAppointmentsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResult<Client>> GetPagedAsync(int page, int pageSize, Guid? establishmentId = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Client>> SearchAsync(Guid? establishmentId, string? name, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Client> CreateAsync(Client entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Client entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Client entity, CancellationToken cancellationToken = default);
}
