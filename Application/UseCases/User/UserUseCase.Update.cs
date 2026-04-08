using System;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.User;

public partial class UserUseCase
{
    public async Task<bool> UpdateAsync(Guid id, UpdateUserDto request, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity == null) return false;
        entity.Update(request.Name.Trim(), request.Email.Trim().ToLowerInvariant(), request.PhoneNumber?.Trim());
        repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return true;
    }
}