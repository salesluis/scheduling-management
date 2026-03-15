using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Entities;
using scheduling_management.Infrastructure.Repositories;
using ProfessionalServiceEntity = scheduling_management.Domain.Entities.ProfessionalService;

namespace scheduling_management.Application.Services;

public class ProfessionalServiceHandle : IProfessionalService
{
    private readonly IProfessionalRepository _repository;
    private readonly IProfessionalServiceRepository _professionalServiceRepository;

    public ProfessionalServiceHandle(IProfessionalRepository repository, IProfessionalServiceRepository professionalServiceRepository)
    {
        _repository = repository;
        _professionalServiceRepository = professionalServiceRepository;
    }

    public async Task<ProfessionalDto> CreateAsync(CreateProfessionalDto request, CancellationToken cancellationToken = default)
    {
        var entity = new Professional(request.EstablishmentId, request.UserId, request.DisplayName.Trim());
        var created = await _repository.CreateAsync(entity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateProfessionalDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdForUpdateAsync(id, cancellationToken);
        if (entity == null) return false;
        entity.SetDisplayName(request.DisplayName.Trim());
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

    public async Task<ProfessionalDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity == null ? null : MapToDto(entity);
    }

    public async Task<PagedResponse<ProfessionalDto>> ListAsync(int page, int pageSize, Guid? establishmentId = null, CancellationToken cancellationToken = default)
    {
        var paged = await _repository.GetPagedAsync(page, pageSize, establishmentId, cancellationToken);
        var items = paged.Items.Select(MapToDto).ToList();
        return new PagedResponse<ProfessionalDto>(items, page, pageSize, paged.TotalCount);
    }

    public async Task<IReadOnlyList<ProfessionalDto>> SearchAsync(Guid? establishmentId, string? displayName, bool? isActive, CancellationToken cancellationToken = default)
    {
        var items = await _repository.SearchAsync(establishmentId, displayName, isActive, cancellationToken);
        return items.Select(MapToDto).ToList();
    }

    public async Task<bool> ActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdForUpdateAsync(id, cancellationToken);
        if (entity == null) return false;
        entity.Activate();
        await _repository.UpdateAsync(entity, cancellationToken);
        return true;
    }

    public async Task<bool> DeactivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdForUpdateAsync(id, cancellationToken);
        if (entity == null) return false;
        entity.Deactivate();
        await _repository.UpdateAsync(entity, cancellationToken);
        return true;
    }

    public async Task<IReadOnlyList<ProfessionalServiceLinkDto>> ListProfessionalServicesAsync(Guid professionalId, CancellationToken cancellationToken = default)
    {
        var items = await _professionalServiceRepository.GetByProfessionalIdAsync(professionalId, cancellationToken);
        return items.Select(MapToLinkDto).ToList();
    }

    private static ProfessionalDto MapToDto(Professional p) => new(
        p.Id,
        p.EstablishmentId,
        p.UserId,
        p.DisplayName,
        p.IsActive,
        p.CreatedAtUtc,
        p.UpdatedAtUtc);

    private static ProfessionalServiceLinkDto MapToLinkDto(ProfessionalServiceEntity ps) => new(
        ps.Id,
        ps.EstablishmentId,
        ps.ProfessionalId,
        ps.ServiceId,
        ps.CreatedAtUtc,
        ps.UpdatedAtUtc);
}
