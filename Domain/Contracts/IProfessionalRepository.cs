using scheduling_management.Domain.Entities;

namespace scheduling_management.Infrastructure.Repositories;

public interface IProfessionalRepository
{
    Task<Professional?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Professional?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Professional?> GetByIdWithAppointmentsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Professional?> GetByIdWithProfessionalServicesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResult<Professional>> GetPagedAsync(int page, int pageSize, Guid? establishmentId = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Professional>> SearchAsync(Guid? establishmentId, string? displayName, bool? isActive, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Professional> CreateAsync(Professional entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Professional entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Professional entity, CancellationToken cancellationToken = default);
}
