using System;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Builders;

namespace scheduling_management.Application.UseCases.Appointment;

public partial class AppointmentUseCase
{
    public async Task<Result<ResponseAppointmentDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity == null
            ? Result<ResponseAppointmentDto>.Fail("NOT_FOUND", "Appointment not found.", ResultErrorType.NotFound)
            : Result<ResponseAppointmentDto>.Ok(MapToResponse(entity));
    }
}