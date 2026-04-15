using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using scheduling_management.Domain.Contracts.Repositories;
using scheduling_management.Domain.Entities;
using scheduling_management.Infra.Data;

namespace scheduling_management.Infra.Repositories;

public class ProfessionalServiceRepository(SchedulingManagementDbContext context)
    : Repository<ProfessionalService>(context), IProfessionalServiceRepository
{
    public async Task<List<ProfessionalService>> GetByProfessionalIdAsync(Guid professionalId, CancellationToken cancellationToken)
    {
        var items = await context
            .ProfessionalServices
            .AsNoTracking()
            .Where(ps => ps.ProfessionalId == professionalId)
            .ToListAsync(cancellationToken);

        return items;
    }

    public async Task<List<ProfessionalService>> GetByServiceIdAsync(Guid serviceId, CancellationToken cancellationToken)
    {
        var items = await context
            .ProfessionalServices
            .AsNoTracking()
            .Where(ps => ps.ServiceId == serviceId)
            .ToListAsync(cancellationToken);

        return items;
    }
}

