using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Entities;
using scheduling_management.Infrastructure.Repositories;

namespace scheduling_management.Application.Services;

public class EstablishmentService : IEstablishmentService
{
    private readonly IEstablishmentRepository _repository;

    public EstablishmentService(IEstablishmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<EstablishmentDto> CreateAsync(CreateEstablishmentDto request, CancellationToken cancellationToken = default)
    {
        var entity = new Establishment(request.Name.Trim());
        var created = await _repository.CreateAsync(entity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateEstablishmentDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdForUpdateAsync(id, cancellationToken);
        if (entity == null) return false;
        entity.UpdateName(request.Name.Trim());
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

    public async Task<EstablishmentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity == null ? null : MapToDto(entity);
    }

    public async Task<PagedResponse<EstablishmentDto>> ListAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var paged = await _repository.GetPagedAsync(page, pageSize, cancellationToken);
        var items = paged.Items.Select(MapToDto).ToList();
        return new PagedResponse<EstablishmentDto>(items, page, pageSize, paged.TotalCount);
    }

    public async Task<IReadOnlyList<EstablishmentDto>> SearchAsync(string? name, bool? isActive, CancellationToken cancellationToken = default)
    {
        var items = await _repository.SearchAsync(name, isActive, cancellationToken);
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

    private static EstablishmentDto MapToDto(Establishment e) => new(
        e.Id,
        e.Name,
        e.Slug,
        e.IsActive,
        e.CreatedAtUtc,
        e.UpdatedAtUtc);
}
