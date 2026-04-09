using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Service;

public interface IServiceUseCase
{
    Task<Result<ServiceResponseDto>> CreateAsync(CreateServiceDto request, CancellationToken cancellationToken = default);
    Task<Result<List<ServiceResponseDto>>> GetAllAsync(Guid establishmentId, CancellationToken cancellationToken = default);
    Task<Result<ServiceResponseDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<Unit>> UpdateAsync(Guid id, UpdateServiceDto request, CancellationToken cancellationToken = default);
    Task<Result<Unit>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<Unit>> ActivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<Unit>> DeactivateAsync(Guid id, CancellationToken cancellationToken = default);
}