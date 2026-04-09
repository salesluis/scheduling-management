using System;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Establishment;

public partial class EstablishmentUseCase
{
    public async Task<Result<ResponseEstablishmentDto>> GetByIdAsync(Guid establishmentId, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(establishmentId, cancellationToken);
        return entity == null
            ? Result<ResponseEstablishmentDto>.Fail("NOT_FOUND", "Establishment not found.", ResultErrorType.NotFound)
            : Result<ResponseEstablishmentDto>.Ok(MapToDto(entity));
    }
}