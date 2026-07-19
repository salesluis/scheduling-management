using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Contracts;
using scheduling_management.Domain.Repository.Abstractions;

namespace scheduling_management.Application.UseCases.Client;

public partial class ClientUseCase(
    IClientRepository repository,
    IUnitOfWork unitOfWork)
    : IClientUseCase
{
    private static ClientResponseDto MapToDto(Domain.Entities.Client c) => new(
        c.Id,
        c.EstablishmentId,
        c.UserId,
        c.Name,
        c.PhoneNumber);

    
}
