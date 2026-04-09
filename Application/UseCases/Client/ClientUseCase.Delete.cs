using System;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Client;

public partial class ClientUseCase
{
    public async Task<Result<Unit>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return Result<Unit>.Fail("NOT_FOUND", "Client not found.", ResultErrorType.NotFound);

        repository.DeleteAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return Result<Unit>.Ok(Unit.Value);
    }
}