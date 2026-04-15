using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Service;

public partial class ServiceUseCase
{
    public async Task<Result<ServiceResponseDto>> CreateAsync(CreateServiceDto request, CancellationToken cancellationToken = default)
    {
        var entity = new Domain.Entities.Service(request.EstablishmentId, request.Name.Trim(), request.DurationInMinutes, request.PriceInReal, request.Color?.Trim(), request.Description?.Trim());
        var created = await repository.CreateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return Result<ServiceResponseDto>.Ok(MapToDto(created));
    }
}