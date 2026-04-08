using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Builders;

namespace scheduling_management.Application.UseCases.Appointment;

public partial class AppointmentUseCase
{
    public Task<List<ResponseAppointmentDto>> GetAllAsync(Guid establishmentId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}