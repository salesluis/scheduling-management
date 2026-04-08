using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Contracts;

namespace scheduling_management.Application.UseCases.Professional;

public interface IProfessionalUseCase
{
    Task<ProfessionalResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<ProfessionalResponseDto>> GetAllAsync(Guid establishmentId, CancellationToken cancellationToken = default);
    Task<bool> ActivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> DeactivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ProfessionalResponseDto> CreateAsync(CreateProfessionalDto request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, UpdateProfessionalDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
