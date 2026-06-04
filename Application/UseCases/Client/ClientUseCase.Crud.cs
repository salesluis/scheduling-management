using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Client;

public partial class ClientUseCase
{
    public async Task<ClienResponsetDto> CreateAsync(CreateClientDto request, CancellationToken cancellationToken = default)
    {
        var entity = new Domain.Entities.Client(request.EstablishmentId, request.Name.Trim(), request.PhoneNumber?.Trim());
        var created = await repository.CreateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return MapToDto(created);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity == null) return false;
        repository.DeleteAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return true;
    }

    public Task<List<ClienResponsetDto>> GetAllAsync(Guid? establishmentId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<ClienResponsetDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity == null ? null : MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateClientDto request, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;
        entity.Update(request.Name.Trim(), request.PhoneNumber?.Trim());
        repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return true;
    }
}
