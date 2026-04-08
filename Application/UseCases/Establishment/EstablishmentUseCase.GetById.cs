using System;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Establishment;

public partial class EstablishmentUseCase
{
    public async Task<ResponseEstablishmentDto?> GetByIdAsync(Guid establishmentId, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(establishmentId, cancellationToken);
        return entity == null ? null : MapToDto(entity);
    }
}