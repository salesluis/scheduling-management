using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Builders;
using scheduling_management.Domain.Contracts;
using scheduling_management.Domain.Contracts.Repositories;

namespace scheduling_management.Application.UseCases.Appointment;

public partial class AppointmentUseCase(
    IAppointmentRepository repository,
    IUnitOfWork unitOfWork)
    : IAppointmentUseCase
{
    //todo: implementar o UseCase para puxar os agendamentos da semana inteira já filtrado
    private static ResponseAppointmentDto MapToResponse(Domain.Entities.Appointment a) => new ResponseAppointmentDto(
        a.Id,
        a.EstablishmentId,
        a.ProfessionalId,
        a.ServiceId,
        a.ClientId,
        a.SchedulingDateOnly,
        a.StartHours,
        a.EndHours,
        a.Status);
}
