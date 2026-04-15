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
    public async Task<Result<List<ClienResponsetDto>>> GetAllAsync(Guid? establishmentId, CancellationToken cancellationToken = default)
    {
        var items = await repository.GetAllAsync(establishmentId ?? Guid.Empty, cancellationToken);
        return Result<List<ClienResponsetDto>>.Ok(items.Select(MapToDto).ToList());
    }
}