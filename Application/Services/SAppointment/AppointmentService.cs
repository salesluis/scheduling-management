using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Contracts;
using scheduling_management.Domain.Entities;
using scheduling_management.Infrastructure.Repositories;

namespace scheduling_management.Application.Services.SAppointment;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;

    public AppointmentService(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseAppointmentDto> CreateAsync(CreateAppointmentDto request, CancellationToken cancellationToken = default)
    {
        var entity = new Appointment(
            request.EstablishmentId,
            request.ProfessionalId,
            request.ServiceId,
            request.ClientId,
            request.SchedulingDateOnly,
            request.StartHours,
            request.EndHours);
        
        var created = await _repository.CreateAsync(entity, cancellationToken);
        return MapToResponse(created);
    }

    public async Task<bool> UpdateAsync( UpdateAppointmentDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdForUpdateAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        entity.Reschedule(request.SchedulingDateOnly, request.StartHours, request.EndHours);
        entity.SetTotalClients(request.TotalClients);
        await _repository.UpdateAsync(entity, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity == null) return false;
        await _repository.DeleteAsync(entity, cancellationToken);
        return true;
    }

    public async Task<ResponseAppointmentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity == null ? null : MapToDto(entity);
    }

    public async Task<PagedResponse<ResponseAppointmentDto>> ListAsync(int page, int pageSize, Guid? establishmentId = null, Guid? professionalId = null, Guid? clientId = null, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default)
    {
        var paged = await _repository.GetPagedAsync(page, pageSize, establishmentId, professionalId, clientId, fromDate, toDate, cancellationToken);
        var items = paged.Items.Select(MapToDto).ToList();
        return new PagedResponse<ResponseAppointmentDto>(items, page, pageSize, paged.TotalCount);
    }

    public async Task<IReadOnlyList<ResponseAppointmentDto>> SearchAsync(Guid? establishmentId, Guid? professionalId, Guid? clientId, DateOnly? fromDate, DateOnly? toDate, int? status, CancellationToken cancellationToken = default)
    {
        var items = await _repository.SearchAsync(establishmentId, professionalId, clientId, fromDate, toDate, status, cancellationToken);
        return items.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<ResponseAppointmentDto>> ListByClientAsync(Guid clientId, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default)
    {
        var items = await _repository.GetByClientIdAsync(clientId, fromDate, toDate, cancellationToken);
        return items.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<ResponseAppointmentDto>> ListByProfessionalAsync(Guid professionalId, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default)
    {
        var items = await _repository.GetByProfessionalIdAsync(professionalId, fromDate, toDate, cancellationToken);
        return items.Select(MapToDto).ToList();
    }

    public async Task<bool> CancelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdForUpdateAsync(id, cancellationToken);
        if (entity == null) return false;
        entity.Cancel();
        await _repository.UpdateAsync(entity, cancellationToken);
        return true;
    }

    public async Task<bool> CompleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdForUpdateAsync(id, cancellationToken);
        if (entity == null) return false;
        entity.Complete();
        await _repository.UpdateAsync(entity, cancellationToken);
        return true;
    }

    private static ResponseAppointmentDto MapToResponse(Appointment a) => new ResponseAppointmentDto(
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
