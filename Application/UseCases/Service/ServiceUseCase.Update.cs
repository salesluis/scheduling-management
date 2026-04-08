using System;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Service;

public partial class ServiceUseCase
{
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