using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Builders;

namespace scheduling_management.Application.UseCases.Appointment;

public partial class AppointmentUseCase
{
    public async Task<List<ResponseAppointmentDto>> GetByClientAsync(Guid clientId, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default)
    {
        var items = await repository.GetByClientIdAsync(clientId, cancellationToken);
        var query = items.AsQueryable();
        if (fromDate.HasValue)
            query = query.Where(a => a.SchedulingDateOnly >= fromDate.Value);
        if (toDate.HasValue)
            query = query.Where(a => a.SchedulingDateOnly <= toDate.Value);
        return query.AsEnumerable().Select(MapToResponse).ToList();
    }

}