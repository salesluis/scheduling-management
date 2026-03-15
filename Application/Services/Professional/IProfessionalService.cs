using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.Services;

public interface IProfessionalService
{
    Task<ProfessionalDto> CreateAsync(CreateProfessionalDto request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, UpdateProfessionalDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ProfessionalDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResponse<ProfessionalDto>> ListAsync(int page, int pageSize, Guid? establishmentId = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProfessionalDto>> SearchAsync(Guid? establishmentId, string? displayName, bool? isActive, CancellationToken cancellationToken = default);
    Task<bool> ActivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> DeactivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProfessionalServiceLinkDto>> ListProfessionalServicesAsync(Guid professionalId, CancellationToken cancellationToken = default);
}
