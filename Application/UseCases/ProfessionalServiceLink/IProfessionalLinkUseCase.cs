using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.ProfessionalServiceLink;

public interface IProfessionalLinkUseCase
{
    Task<Result<ResponseProfessionalServiceDto>> CreateAsync(CreateProfessionalServiceDto request, CancellationToken cancellationToken = default);
    Task<Result<PagedResponse<ResponseProfessionalServiceDto>>> ListAsync(int page, int pageSize, Guid? establishmentId, Guid? professionalId, Guid? serviceId, CancellationToken cancellationToken = default);
    Task<Result<ResponseProfessionalServiceDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<Unit>> UpdateAsync(Guid id, UpdateProfessionalServiceDto request, CancellationToken cancellationToken = default);
    Task<Result<Unit>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyList<ResponseProfessionalServiceDto>>> SearchAsync(Guid? establishmentId, Guid? professionalId, Guid? serviceId, CancellationToken cancellationToken = default);
}