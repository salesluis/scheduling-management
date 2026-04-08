using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Service;

public interface IServiceUseCase
{
    Task<ServiceResponseDto> CreateAsync(CreateServiceDto request, CancellationToken cancellationToken = default);
    Task<List<ServiceResponseDto>> GetAllAsync( Guid establishmentId, CancellationToken cancellationToken = default);
    Task<ServiceResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, UpdateServiceDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ActivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> DeactivateAsync(Guid id, CancellationToken cancellationToken = default);
}