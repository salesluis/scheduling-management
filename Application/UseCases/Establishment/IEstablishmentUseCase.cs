using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Contracts;

namespace scheduling_management.Application.UseCases.Establishment;

public interface IEstablishmentUseCase
{
    Task<ResponseEstablishmentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<ResponseEstablishmentDto>> GetAllAsync(Guid establishment, CancellationToken cancellationToken = default);
    Task<bool> ActivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> DeactivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ResponseEstablishmentDto> CreateAsync(CreateEstablishmentDto request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, UpdateEstablishmentDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

