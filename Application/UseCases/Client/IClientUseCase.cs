using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Client;

public interface IClientUseCase
{
    Task<Result<ClientResponseDto>> CreateAsync(CreateClientDto request, CancellationToken cancellationToken = default);
    Task<Result<Unit>> UpdateAsync(Guid id, UpdateClientDto request, CancellationToken cancellationToken = default);
    Task<Result<Unit>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<ClientResponseDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<List<ClientResponseDto>>> GetAllAsync(Guid? establishmentId, CancellationToken cancellationToken = default);
}
