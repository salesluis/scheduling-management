using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Client;

public partial class ClientUseCase
{
    public Task<List<ClienResponsetDto>> GetAllAsync(Guid? establishmentId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}