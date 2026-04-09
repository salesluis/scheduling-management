using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Service;

public partial class ServiceUseCase
{
    public async Task<Result<List<ServiceResponseDto>>> GetAllAsync(Guid establishmentId,
        CancellationToken cancellationToken = default)
    {
        var all = await repository.GetAllAsync(establishmentId , cancellationToken);
        var items = all.Select(MapToDto).ToList();
        return Result<List<ServiceResponseDto>>.Ok(items);
    }
    
}