using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Contracts;
using scheduling_management.Domain.Contracts.Repositories;

namespace scheduling_management.Application.UseCases.Client;

public partial class ClientUseCase(
    IClientRepository repository,
    IUnitOfWork unitOfWork)
    : IClientUseCase
{
    private static ClienResponsetDto MapToDto(Domain.Entities.Client c) => new(
        c.Id,
        c.EstablishmentId,
        c.UserId,
        c.Name,
        c.PhoneNumber);

    
}
