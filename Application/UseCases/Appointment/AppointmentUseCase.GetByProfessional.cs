using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Appointment;

public partial class AppointmentUseCase
{
    public async Task<Result<List<ResponseAppointmentDto>>> GetByProfessionalAsync(Guid professionalId, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default)
    {
        var items = await repository.GetByProfessionalIdAsync(professionalId, cancellationToken);
        var query = items.AsQueryable();
        if (fromDate.HasValue)
            query = query.Where(a => a.SchedulingDateOnly >= fromDate.Value);
        if (toDate.HasValue)
            query = query.Where(a => a.SchedulingDateOnly <= toDate.Value);

        return Result<List<ResponseAppointmentDto>>.Ok(query.Select(MapToResponse).ToList());
    }
}