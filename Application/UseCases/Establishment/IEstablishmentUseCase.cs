using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Contracts;

namespace scheduling_management.Application.UseCases.Establishment;

public interface IEstablishmentUseCase
{
    Task<Result<ResponseEstablishmentDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<List<ResponseEstablishmentDto>>> GetAllAsync(Guid establishment, CancellationToken cancellationToken = default);
    Task<Result<Unit>> ActivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<Unit>> DeactivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<ResponseEstablishmentDto>> CreateAsync(CreateEstablishmentDto request, CancellationToken cancellationToken = default);
    Task<Result<Unit>> UpdateAsync(Guid id, UpdateEstablishmentDto request, CancellationToken cancellationToken = default);
    Task<Result<Unit>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

