using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Contracts;

namespace scheduling_management.Application.UseCases.Client;

public interface IClientUseCase
{
    Task<Result<ClienResponsetDto>> CreateAsync(CreateClientDto request, CancellationToken cancellationToken = default);
    Task<Result<Unit>> UpdateAsync(Guid id, UpdateClientDto request, CancellationToken cancellationToken = default);
    Task<Result<Unit>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<ClienResponsetDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<List<ClienResponsetDto>>> GetAllAsync(Guid? establishmentId, CancellationToken cancellationToken = default);
}
