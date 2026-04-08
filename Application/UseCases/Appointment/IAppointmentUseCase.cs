using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Appointment;

public interface IAppointmentUseCase
{
    Task<ResponseAppointmentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<ResponseAppointmentDto>> GetAllAsync(Guid establishmentId, CancellationToken cancellationToken = default);
    Task<List<ResponseAppointmentDto>> GetByClientAsync(Guid clientId, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default);
    Task<List<ResponseAppointmentDto>> GetByProfessionalAsync(Guid professionalId, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default);
    Task<bool> CancelAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> CompleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ResponseAppointmentDto> CreateAsync(CreateAppointmentDto request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, UpdateAppointmentDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
