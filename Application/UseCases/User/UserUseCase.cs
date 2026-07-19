using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Contracts;
using scheduling_management.Domain.Repository.Abstractions;

namespace scheduling_management.Application.UseCases.User;

public partial class UserUseCase(
    IUserRepository repository,
    IUnitOfWork unitOfWork)
    : IUserUseCase
{
    private static UserResponseDto MapToDto(Domain.Entities.User u) => new(
        u.Id,
        u.Name,
        u.Email);
}
