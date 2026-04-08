using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Client;

public partial class ClientUseCase
{
    public async Task<ClienResponsetDto> CreateAsync(CreateClientDto request, CancellationToken cancellationToken = default)
    {
        var entity = new Domain.Entities.Client(request.EstablishmentId, request.Name.Trim(), request.PhoneNumber?.Trim());
        var created = await repository.CreateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return MapToDto(created);
    }
}