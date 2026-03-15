using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.Services;

public interface IAppointmentService
{
    Task<ResponseAppointmentDto> CreateAsync(CreateAppointmentDto request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, UpdateAppointmentDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ResponseAppointmentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResponse<ResponseAppointmentDto>> ListAsync(int page, int pageSize, Guid? establishmentId = null, Guid? professionalId = null, Guid? clientId = null, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ResponseAppointmentDto>> SearchAsync(Guid? establishmentId, Guid? professionalId, Guid? clientId, DateOnly? fromDate, DateOnly? toDate, int? status, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ResponseAppointmentDto>> ListByClientAsync(Guid clientId, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ResponseAppointmentDto>> ListByProfessionalAsync(Guid professionalId, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default);
    Task<bool> CancelAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> CompleteAsync(Guid id, CancellationToken cancellationToken = default);
}
