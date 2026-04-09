using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Contracts;

namespace scheduling_management.Application.UseCases.Professional;

public interface IProfessionalUseCase
{
    Task<Result<ProfessionalResponseDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<List<ProfessionalResponseDto>>> GetAllAsync(Guid establishmentId, CancellationToken cancellationToken = default);
    Task<Result<Unit>> ActivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<Unit>> DeactivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<ProfessionalResponseDto>> CreateAsync(CreateProfessionalDto request, CancellationToken cancellationToken = default);
    Task<Result<Unit>> UpdateAsync(Guid id, UpdateProfessionalDto request, CancellationToken cancellationToken = default);
    Task<Result<Unit>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
