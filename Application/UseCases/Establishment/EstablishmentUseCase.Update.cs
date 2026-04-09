using System;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Establishment;

public partial class EstablishmentUseCase
{
    public async Task<Result<Unit>> UpdateAsync(Guid id, UpdateEstablishmentDto request, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return Result<Unit>.Fail("NOT_FOUND", "Establishment not found.", ResultErrorType.NotFound);

        entity.UpdateName(request.Name.Trim());
        repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return Result<Unit>.Ok(Unit.Value);
    }
}