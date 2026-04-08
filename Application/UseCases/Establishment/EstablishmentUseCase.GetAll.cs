using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Application.UseCases.Establishment;

public partial class EstablishmentUseCase
{
    public async Task<List<ResponseEstablishmentDto>> GetAllAsync(Guid establishmentId, CancellationToken cancellationToken = default)
    {
        var all = await repository.GetAllEstablishment(cancellationToken);
        var items = all.Select(MapToDto).ToList();
        return items;
    }
}