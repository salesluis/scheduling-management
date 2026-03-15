using scheduling_management.Domain.Entities;

namespace scheduling_management.Infrastructure.Repositories;

public interface IProfessionalServiceRepository
{
    Task<ProfessionalService?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ProfessionalService?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResult<ProfessionalService>> GetPagedAsync(int page, int pageSize, Guid? establishmentId = null, Guid? professionalId = null, Guid? serviceId = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProfessionalService>> GetByProfessionalIdAsync(Guid professionalId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProfessionalService>> GetByServiceIdAsync(Guid serviceId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProfessionalService>> SearchAsync(Guid? establishmentId, Guid? professionalId, Guid? serviceId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ProfessionalService> CreateAsync(ProfessionalService entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(ProfessionalService entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(ProfessionalService entity, CancellationToken cancellationToken = default);
}
