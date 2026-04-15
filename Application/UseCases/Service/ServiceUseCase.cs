using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Contracts;
using scheduling_management.Domain.Contracts.Repositories;

namespace scheduling_management.Application.UseCases.Service;

public partial class ServiceUseCase(IServiceRepository repository, IUnitOfWork unitOfWork)
    : IServiceUseCase
{
    //todo: ao invés de ter dois useCases que ativa e desativa, verificar possibilidade de ter apenas um método
    // Ex: ToggleActivate 
    public async Task<Result<Unit>> ActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return Result<Unit>.Fail("NOT_FOUND", "Service not found.", ResultErrorType.NotFound);

        entity.Activate();
        repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return Result<Unit>.Ok(Unit.Value);
    }

    public async Task<Result<Unit>> DeactivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return Result<Unit>.Fail("NOT_FOUND", "Service not found.", ResultErrorType.NotFound);

        entity.Deactivate();
        repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return Result<Unit>.Ok(Unit.Value);
    }

    private static ServiceResponseDto MapToDto(Domain.Entities.Service s) => new(
        s.Id,
        s.EstablishmentId,
        s.Name,
        s.DurationInMinutes,
        s.PriceInReal,
        s.IsActive,
        s.Color,
        s.Description);
}