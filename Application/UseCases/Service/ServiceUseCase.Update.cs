using System;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Service;

public partial class ServiceUseCase
{
    public async Task<Result<Unit>> UpdateAsync(Guid id, UpdateServiceDto request, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return Result<Unit>.Fail("NOT_FOUND", "Service not found.", ResultErrorType.NotFound);

        entity.Update(request.Name.Trim(), request.DurationInMinutes, request.PriceInReal, request.Color?.Trim(), request.Description?.Trim());
        repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return Result<Unit>.Ok(Unit.Value);
    }
}