using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Appointment;

public partial class AppointmentUseCase
{
    Task<List<ResponseAppointmentDto>> IAppointmentUseCase.GetByProfessionalAsync(Guid professionalId, DateOnly? fromDate, DateOnly? toDate,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}