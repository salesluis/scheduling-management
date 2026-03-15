using scheduling_management.Domain.Entities;

namespace scheduling_management.Infrastructure.Repositories;

public interface IAppointmentRepository
{
    Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Appointment?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResult<Appointment>> GetPagedAsync(int page, int pageSize, Guid? establishmentId = null, Guid? professionalId = null, Guid? clientId = null, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Appointment>> GetByProfessionalIdAsync(Guid professionalId, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Appointment>> GetByClientIdAsync(Guid clientId, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Appointment>> GetByEstablishmentIdAsync(Guid establishmentId, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Appointment>> SearchAsync(Guid? establishmentId, Guid? professionalId, Guid? clientId, DateOnly? fromDate, DateOnly? toDate, int? status, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Appointment> CreateAsync(Appointment entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Appointment entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Appointment entity, CancellationToken cancellationToken = default);
}
