using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Service;

public partial class ServiceUseCase
{
    public async Task<ServiceResponseDto> CreateAsync(CreateServiceDto request, CancellationToken cancellationToken = default)
    {
        var entity = new Domain.Entities.Service(request.EstablishmentId, request.Name.Trim(), request.DurationInMinutes, request.PriceInReal, request.Color?.Trim(), request.Description?.Trim());
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

    public async Task<List<ServiceResponseDto>> GetAllAsync(Guid establishmentId, CancellationToken cancellationToken = default)
    {
        var all = await repository.GetAllAsync(establishmentId , cancellationToken);
        return all.Select(MapToDto).ToList();
    }

    public async Task<ServiceResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity == null ? null : MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateServiceDto request, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity == null) return false;
        entity.Update(request.Name.Trim(), request.DurationInMinutes, request.PriceInReal, request.Color?.Trim(), request.Description?.Trim());
        repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return true;
    }
}
