using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Entities;
using scheduling_management.Infrastructure.Repositories;

namespace scheduling_management.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _repository;

    public ClientService(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseClientDto> CreateAsync(CreateClientDto request, CancellationToken cancellationToken = default)
    {
        var entity = new Client(request.EstablishmentId, request.Name.Trim(), request.PhoneNumber?.Trim());
        var created = await _repository.CreateAsync(entity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateClientDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdForUpdateAsync(id, cancellationToken);
        if (entity == null) return false;
        entity.Update(request.Name.Trim(), request.PhoneNumber?.Trim());
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

    public async Task<ResponseClientDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity == null ? null : MapToDto(entity);
    }

    public async Task<PagedResponse<ResponseClientDto>> ListAsync(int page, int pageSize, Guid? establishmentId = null, CancellationToken cancellationToken = default)
    {
        var paged = await _repository.GetPagedAsync(page, pageSize, establishmentId, cancellationToken);
        var items = paged.Items.Select(MapToDto).ToList();
        return new PagedResponse<ResponseClientDto>(items, page, pageSize, paged.TotalCount);
    }

    public async Task<IReadOnlyList<ResponseClientDto>> SearchAsync(Guid? establishmentId, string? name, CancellationToken cancellationToken = default)
    {
        var items = await _repository.SearchAsync(establishmentId, name, cancellationToken);
        return items.Select(MapToDto).ToList();
    }

    private static ResponseClientDto MapToDto(Client c) => new(
        c.Id,
        c.EstablishmentId,
        c.UserId,
        c.Name,
        c.PhoneNumber,
        c.CreatedAtUtc,
        c.UpdatedAtUtc);
}
