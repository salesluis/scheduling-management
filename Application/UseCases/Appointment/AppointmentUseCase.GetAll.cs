using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Builders;

namespace scheduling_management.Application.UseCases.Appointment;

public partial class AppointmentUseCase
{
    public async Task<Result<List<ResponseAppointmentDto>>> GetAllAsync(Guid establishmentId, CancellationToken cancellationToken = default)
    {
        var items = await repository.GetAllAsync(establishmentId, cancellationToken);
        return Result<List<ResponseAppointmentDto>>.Ok(items.Select(MapToResponse).ToList());
    }
}