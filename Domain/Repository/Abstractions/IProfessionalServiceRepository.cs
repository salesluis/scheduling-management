using scheduling_management.Domain.Entities;

namespace scheduling_management.Domain.Repository.Abstractions;

public interface IProfessionalServiceRepository :  IRepository<ProfessionalService>
{
    Task<List<ProfessionalService>> GetByProfessionalIdAsync(Guid professionalId, CancellationToken cancellationToken);
    Task<List<ProfessionalService>> GetByServiceIdAsync(Guid serviceId, CancellationToken cancellationToken);
}
