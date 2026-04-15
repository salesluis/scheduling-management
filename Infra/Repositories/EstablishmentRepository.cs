using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using scheduling_management.Domain.Contracts.Repositories;
using scheduling_management.Domain.Entities;
using scheduling_management.Infra.Data;
using scheduling_management.Infra.Data;

namespace scheduling_management.Infra.Repositories;

public class EstablishmentRepository(SchedulingManagementDbContext context)
    : Repository<Establishment>(context), IEstablishmentRepository
{
    public async Task<List<Establishment>> GetAllEstablishment(CancellationToken ct = default)
    {
        var establishments = await context
            .Establishments
            .AsNoTracking()
            .ToListAsync(ct);

        return establishments;
    }
}
