using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Entities;
using scheduling_management.Infrastructure.Repositories;

namespace scheduling_management.Application.Services;

public class ProfessionalServiceLinkService : IProfessionalServiceLinkService
{
    private readonly IProfessionalServiceRepository _repository;

    public ProfessionalServiceLinkService(IProfessionalServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProfessionalServiceDto> CreateAsync(CreateProfessionalServiceDto request, CancellationToken cancellationToken = default)
    {
        var entity = new ProfessionalService(request.EstablishmentId, request.ProfessionalId, request.ServiceId);
        var created = await _repository.CreateAsync(entity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateProfessionalServiceDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdForUpdateAsync(id, cancellationToken);
        if (entity == null) return false;
        entity.Update(request.ProfessionalId, request.ServiceId);
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

    public async Task<ProfessionalServiceDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity == null ? null : MapToDto(entity);
    }

    public async Task<PagedResponse<ProfessionalServiceDto>> ListAsync(int page, int pageSize, Guid? establishmentId = null, Guid? professionalId = null, Guid? serviceId = null, CancellationToken cancellationToken = default)
    {
        var paged = await _repository.GetPagedAsync(page, pageSize, establishmentId, professionalId, serviceId, cancellationToken);
        var items = paged.Items.Select(MapToDto).ToList();
        return new PagedResponse<ProfessionalServiceDto>(items, page, pageSize, paged.TotalCount);
    }

    public async Task<IReadOnlyList<ProfessionalServiceDto>> SearchAsync(Guid? establishmentId, Guid? professionalId, Guid? serviceId, CancellationToken cancellationToken = default)
    {
        var items = await _repository.SearchAsync(establishmentId, professionalId, serviceId, cancellationToken);
        return items.Select(MapToDto).ToList();
    }

    private static ProfessionalServiceDto MapToDto(ProfessionalService ps) => new(
        ps.Id,
        ps.EstablishmentId,
        ps.ProfessionalId,
        ps.ServiceId,
        ps.CreatedAtUtc,
        ps.UpdatedAtUtc);
}
