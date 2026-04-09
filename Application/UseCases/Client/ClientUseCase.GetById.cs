using System;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Client;

public partial class ClientUseCase
{
    public async Task<Result<ClienResponsetDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity == null
            ? Result<ClienResponsetDto>.Fail("NOT_FOUND", "Client not found.", ResultErrorType.NotFound)
            : Result<ClienResponsetDto>.Ok(MapToDto(entity));
    }
}