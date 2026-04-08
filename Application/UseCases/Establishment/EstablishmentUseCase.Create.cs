using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Establishment;

public partial class EstablishmentUseCase
{
    public async Task<ResponseEstablishmentDto> CreateAsync(CreateEstablishmentDto request, CancellationToken cancellationToken = default)
    {
        var entity = new Domain.Entities.Establishment(request.Name.Trim());
        var created = await repository.CreateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return MapToDto(created);
    }
}