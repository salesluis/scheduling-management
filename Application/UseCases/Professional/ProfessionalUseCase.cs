using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Contracts;
using scheduling_management.Domain.Contracts.Repositories;

namespace scheduling_management.Application.UseCases.Professional;

public partial class ProfessionalUseCase(
    IProfessionalRepository repository,
    IUnitOfWork unitOfWork)
    : IProfessionalUseCase
{
    public async Task<Result<Unit>> ActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return Result<Unit>.Fail("NOT_FOUND", "Professional not found.", ResultErrorType.NotFound);

        entity.Activate();
        repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return Result<Unit>.Ok(Unit.Value);
    }

    public async Task<Result<Unit>> DeactivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return Result<Unit>.Fail("NOT_FOUND", "Professional not found.", ResultErrorType.NotFound);

        entity.Deactivate();
        repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return Result<Unit>.Ok(Unit.Value);
    }
    
    private static ProfessionalResponseDto MapToDto(Domain.Entities.Professional p) => new(
        p.Id,
        p.EstablishmentId,
        p.UserId,
        p.DisplayName,
        p.IsActive,
        p.CreatedAtUtc,
        p.UpdatedAtUtc);
}


