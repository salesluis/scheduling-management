using System;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.User;

public partial class UserUseCase
{
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity == null) return false;
        repository.DeleteAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return true;
    }
}