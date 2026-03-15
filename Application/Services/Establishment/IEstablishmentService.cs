using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.Services;

public interface IEstablishmentService
{
    Task<EstablishmentDto> CreateAsync(CreateEstablishmentDto request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, UpdateEstablishmentDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<EstablishmentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResponse<EstablishmentDto>> ListAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EstablishmentDto>> SearchAsync(string? name, bool? isActive, CancellationToken cancellationToken = default);
    Task<bool> ActivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> DeactivateAsync(Guid id, CancellationToken cancellationToken = default);
}
