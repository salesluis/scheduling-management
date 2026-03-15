using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.Services;

public interface IServiceCatalogService
{
    Task<ServiceDto> CreateAsync(CreateServiceDto request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, UpdateServiceDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ServiceDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResponse<ServiceDto>> ListAsync(int page, int pageSize, Guid? establishmentId = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ServiceDto>> SearchAsync(Guid? establishmentId, string? name, bool? isActive, CancellationToken cancellationToken = default);
    Task<bool> ActivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> DeactivateAsync(Guid id, CancellationToken cancellationToken = default);
}
