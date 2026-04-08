using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Contracts;

namespace scheduling_management.Application.UseCases.Client;

public interface IClientUseCase
{
    Task<ClienResponsetDto> CreateAsync(CreateClientDto request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, UpdateClientDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ClienResponsetDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<ClienResponsetDto>> GetAllAsync(Guid? establishmentId, CancellationToken cancellationToken = default);
}
