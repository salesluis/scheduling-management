using System;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Service;

public partial class ServiceUseCase
{
    public async Task<ServiceResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity == null ? null : MapToDto(entity);
    }
}