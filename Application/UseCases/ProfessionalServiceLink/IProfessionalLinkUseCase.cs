using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.ProfessionalServiceLink;

public interface IProfessionalLinkUseCase
{
    Task<ResponseProfessionalServiceDto> CreateAsync(CreateProfessionalServiceDto request, CancellationToken cancellationToken = default);
    Task<PagedResponse<ResponseProfessionalServiceDto>> ListAsync(int page, int pageSize, Guid? establishmentId, Guid? professionalId, Guid? serviceId, CancellationToken cancellationToken = default);
    Task<ResponseProfessionalServiceDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, UpdateProfessionalServiceDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ResponseProfessionalServiceDto>> SearchAsync(Guid? establishmentId, Guid? professionalId, Guid? serviceId, CancellationToken cancellationToken = default);
}