using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Contracts;
using scheduling_management.Domain.Contracts.Repositories;

namespace scheduling_management.Application.UseCases.Professional;

public partial class ProfessionalUseCase(
    IProfessionalRepository repository,
    IUnitOfWork unitOfWork)
    : IProfessionalUseCase
{
    public async Task<bool> ActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity == null) return false;
        entity.Activate();
        repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return true;
    }

    public async Task<bool> DeactivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity == null) return false;
        entity.Deactivate();
        repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return true;
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


