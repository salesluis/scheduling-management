using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Client;

public partial class ClientUseCase
{
    public async Task<Result<List<ClientResponseDto>>> GetAllAsync(Guid? establishmentId, CancellationToken cancellationToken = default)
    {
        var items = await repository.GetAllAsync(establishmentId ?? Guid.Empty, cancellationToken);
        return Result<List<ClientResponseDto>>.Ok(items.Select(MapToDto).ToList());
    }
}