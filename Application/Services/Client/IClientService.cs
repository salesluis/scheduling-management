using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.Services;

public interface IClientService
{
    Task<ResponseClientDto> CreateAsync(CreateClientDto request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, UpdateClientDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ResponseClientDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResponse<ResponseClientDto>> ListAsync(int page, int pageSize, Guid? establishmentId = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ResponseClientDto>> SearchAsync(Guid? establishmentId, string? name, CancellationToken cancellationToken = default);
}
