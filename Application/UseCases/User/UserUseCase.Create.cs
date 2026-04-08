using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.User;

public partial class UserUseCase
{
    public async Task<UserResponseDto> CreateAsync(CreateUserDto request, CancellationToken cancellationToken = default)
    {
        var entity = new Domain.Entities.User(request.Name.Trim(), request.Email.Trim().ToLowerInvariant(), request.PhoneNumber?.Trim());
        var created = await repository.CreateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return MapToDto(created);
    }
}