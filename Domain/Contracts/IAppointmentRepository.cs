using scheduling_management.Domain.Entities;

namespace scheduling_management.Domain.Contracts;

public interface IAppointmentRepository
{
    Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Appointment>> GetByProfessionalIdAsync(Guid professionalId, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default);
    Task<List<Appointment>> GetByClientIdAsync(Guid clientId, CancellationToken cancellationToken = default);
    Task<List<Appointment>> GetByEstablishmentIdAsync(Guid establishmentId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Appointment> CreateAsync(Appointment entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Appointment entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
