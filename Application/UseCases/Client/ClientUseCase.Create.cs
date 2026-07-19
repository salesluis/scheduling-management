using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Client;

public partial class ClientUseCase
{
    public async Task<Result<ClientResponseDto>> CreateAsync(CreateClientDto request, CancellationToken cancellationToken = default)
    {
        var entity = new Domain.Entities.Client(request.EstablishmentId, request.Name.Trim(), request.PhoneNumber?.Trim());
        var created = await repository.CreateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return Result<ClientResponseDto>.Ok(MapToDto(created));
    }
}