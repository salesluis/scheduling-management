using System;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Builders;

namespace scheduling_management.Application.UseCases.Appointment;

public partial class AppointmentUseCase
{
    public async Task<ResponseAppointmentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity == null ? null : MapToResponse(entity);
    }
}