using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.Services;

public interface IClientService
{
    Task<ClientDto> CreateAsync(CreateClientDto request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, UpdateClientDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ClientDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResponse<ClientDto>> ListAsync(int page, int pageSize, Guid? establishmentId = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ClientDto>> SearchAsync(Guid? establishmentId, string? name, CancellationToken cancellationToken = default);
}
