using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Entities;
using scheduling_management.Infrastructure.Repositories;

namespace scheduling_management.Application.Services;

public class ServiceCatalogService : IServiceCatalogService
{
    private readonly IServiceRepository _repository;

    public ServiceCatalogService(IServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceDto> CreateAsync(CreateServiceDto request, CancellationToken cancellationToken = default)
    {
        var entity = new Service(
            request.EstablishmentId,
            request.Name.Trim(),
            request.DurationInMinutes,
            request.PriceInReal,
            request.Color,
            request.Description);
        var created = await _repository.CreateAsync(entity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateServiceDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdForUpdateAsync(id, cancellationToken);
        if (entity == null) return false;
        entity.Update(request.Name.Trim(), request.DurationInMinutes, request.PriceInReal, request.Color, request.Description);
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

    public async Task<ServiceDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity == null ? null : MapToDto(entity);
    }

    public async Task<PagedResponse<ServiceDto>> ListAsync(int page, int pageSize, Guid? establishmentId = null, CancellationToken cancellationToken = default)
    {
        var paged = await _repository.GetPagedAsync(page, pageSize, establishmentId, cancellationToken);
        var items = paged.Items.Select(MapToDto).ToList();
        return new PagedResponse<ServiceDto>(items, page, pageSize, paged.TotalCount);
    }

    public async Task<IReadOnlyList<ServiceDto>> SearchAsync(Guid? establishmentId, string? name, bool? isActive, CancellationToken cancellationToken = default)
    {
        var items = await _repository.SearchAsync(establishmentId, name, isActive, cancellationToken);
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

    private static ServiceDto MapToDto(Service s) => new(
        s.Id,
        s.EstablishmentId,
        s.Name,
        s.DurationInMinutes,
        s.PriceInReal,
        s.IsActive,
        s.Color,
        s.Description,
        s.CreatedAtUtc,
        s.UpdatedAtUtc);
}
