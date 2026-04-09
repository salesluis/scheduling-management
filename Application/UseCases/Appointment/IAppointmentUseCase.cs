using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Appointment;

public interface IAppointmentUseCase
{
    Task<Result<ResponseAppointmentDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<List<ResponseAppointmentDto>>> GetAllAsync(Guid establishmentId, CancellationToken cancellationToken = default);
    Task<Result<List<ResponseAppointmentDto>>> GetByClientAsync(Guid clientId, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default);
    Task<Result<List<ResponseAppointmentDto>>> GetByProfessionalAsync(Guid professionalId, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default);
    Task<Result<Unit>> CancelAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<Unit>> CompleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<ResponseAppointmentDto>> CreateAsync(CreateAppointmentDto request, CancellationToken cancellationToken = default);
    Task<Result<Unit>> UpdateAsync(Guid id, UpdateAppointmentDto request, CancellationToken cancellationToken = default);
    Task<Result<Unit>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
