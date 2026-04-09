using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Client;

public partial class ClientUseCase
{
    public async Task<Result<ClienResponsetDto>> CreateAsync(CreateClientDto request, CancellationToken cancellationToken = default)
    {
        var entity = new Domain.Entities.Client(request.EstablishmentId, request.Name.Trim(), request.PhoneNumber?.Trim());
        var created = await repository.CreateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return Result<ClienResponsetDto>.Ok(MapToDto(created));
    }
}