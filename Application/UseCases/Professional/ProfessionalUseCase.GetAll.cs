using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Professional;

public partial class ProfessionalUseCase
{
    public async Task<List<ProfessionalResponseDto>> GetAllAsync(Guid establishmentId, CancellationToken cancellationToken = default)
    {
        var all = await repository.GetAllAsync(establishmentId, cancellationToken);
        var p = all.Select(MapToDto).ToList();
        return p;
    }
}