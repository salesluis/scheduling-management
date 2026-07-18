using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Builders;

namespace scheduling_management.Application.UseCases.Appointment;

public partial class AppointmentUseCase
{
    public async Task<ResponseAppointmentDto> CreateAsync(CreateAppointmentDto request, CancellationToken cancellationToken = default)
    {
        var entity = new AppointmentBuilder()
            .WithEstabilishmentId(request.EstablishmentId)
            .WithClientId(request.ClientId)
            .WithProfessionalId(request.ProfessionalId)
            .WithServicetId(request.ServiceId)
            .WithStartHours(request.StartHours)
            .WithEndHours(request.EndHours)
            .WithSchedulingDateOnly(request.SchedulingDateOnly)
            .Build();

        var created = await repository.CreateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return MapToResponse(created);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity == null) return false;
        repository.DeleteAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return true;
    }

    public async Task<List<ResponseAppointmentDto>> GetAllAsync(Guid establishmentId, CancellationToken cancellationToken = default)
    {
        var entities = await repository.GetAllAsync(establishmentId, cancellationToken);
        
        return entities
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<ResponseAppointmentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity == null ? null : MapToResponse(entity);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateAppointmentDto request, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity == null) return false;
        entity.Reschedule(request.SchedulingDateOnly, request.StartHours, request.EndHours);
        repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return true;
    }
}
