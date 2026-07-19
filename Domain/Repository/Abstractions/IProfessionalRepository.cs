using scheduling_management.Domain.Entities;

namespace scheduling_management.Domain.Repository.Abstractions;

public interface IProfessionalRepository :  IRepository<Professional>
{
    Task<Professional?> GetByIdWithAppointmentsAsync(Guid id, CancellationToken cancellationToken);
    Task<Professional?> GetByIdWithProfessionalServicesAsync(Guid id, CancellationToken cancellationToken);
}
