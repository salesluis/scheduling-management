using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Professional;

public partial class ProfessionalUseCase
{
    public async Task<Result<ProfessionalResponseDto>> CreateAsync(CreateProfessionalDto request,
        CancellationToken cancellationToken = default)
    {
        var entity =
            new Domain.Entities.Professional(request.EstablishmentId, request.UserId, request.DisplayName.Trim());
        var created = await repository.CreateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return Result<ProfessionalResponseDto>.Ok(MapToDto(created));
    }
}