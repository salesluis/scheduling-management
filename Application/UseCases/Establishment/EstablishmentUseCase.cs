using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Contracts;
using scheduling_management.Domain.Contracts.Repositories;

namespace scheduling_management.Application.UseCases.Establishment;

public partial class EstablishmentUseCase(IEstablishmentRepository repository, IUnitOfWork unitOfWork)
    : IEstablishmentUseCase
{
    public async Task<Result<Unit>> ActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return Result<Unit>.Fail("NOT_FOUND", "Establishment not found.", ResultErrorType.NotFound);

        entity.Activate();
        repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return Result<Unit>.Ok(Unit.Value);
    }

    public async Task<Result<Unit>> DeactivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return Result<Unit>.Fail("NOT_FOUND", "Establishment not found.", ResultErrorType.NotFound);

        entity.Deactivate();
        repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return Result<Unit>.Ok(Unit.Value);
    }
    
    private static ResponseEstablishmentDto MapToDto(Domain.Entities.Establishment e) => new(
        e.Id,
        e.Name,
        e.Slug,
        e.IsActive,
        e.CreatedAtUtc,
        e.UpdatedAtUtc);
}
