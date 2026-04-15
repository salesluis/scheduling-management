using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Professional;

public partial class ProfessionalUseCase
{
    public async Task<Result<ProfessionalResponseDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity == null
            ? Result<ProfessionalResponseDto>.Fail("NOT_FOUND", "Professional not found.", ResultErrorType.NotFound)
            : Result<ProfessionalResponseDto>.Ok(MapToDto(entity));
    }
}