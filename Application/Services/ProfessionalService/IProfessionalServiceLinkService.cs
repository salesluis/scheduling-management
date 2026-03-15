using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.Services;

public interface IProfessionalServiceLinkService
{
    Task<ProfessionalServiceDto> CreateAsync(CreateProfessionalServiceDto request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, UpdateProfessionalServiceDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ProfessionalServiceDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResponse<ProfessionalServiceDto>> ListAsync(int page, int pageSize, Guid? establishmentId = null, Guid? professionalId = null, Guid? serviceId = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProfessionalServiceDto>> SearchAsync(Guid? establishmentId, Guid? professionalId, Guid? serviceId, CancellationToken cancellationToken = default);
}
