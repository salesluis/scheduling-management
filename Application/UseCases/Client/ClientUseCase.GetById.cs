using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Client;

public partial class ClientUseCase
{
    public async Task<Result<ClientResponseDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity == null
            ? Result<ClientResponseDto>.Fail("NOT_FOUND", "Client not found.", ResultErrorType.NotFound)
            : Result<ClientResponseDto>.Ok(MapToDto(entity));
    }
}