using System;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Client;

public partial class ClientUseCase
{
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