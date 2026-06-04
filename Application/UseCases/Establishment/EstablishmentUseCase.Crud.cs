using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Establishment;

public partial class EstablishmentUseCase
{
    public async Task<ResponseEstablishmentDto> CreateAsync(CreateEstablishmentDto request, CancellationToken cancellationToken = default)
    {
        var entity = new Domain.Entities.Establishment(request.Name.Trim());
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

    public async Task<List<ResponseEstablishmentDto>> GetAllAsync(Guid establishmentId, CancellationToken cancellationToken = default)
    {
        var all = await repository.GetAllEstablishment(cancellationToken);
        return all.Select(MapToDto).ToList();
    }

    public async Task<ResponseEstablishmentDto?> GetByIdAsync(Guid establishmentId, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(establishmentId, cancellationToken);
        return entity == null ? null : MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateEstablishmentDto request, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity == null) return false;
        entity.UpdateName(request.Name.Trim());
        repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return true;
    }
}
