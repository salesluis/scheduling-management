using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using scheduling_management.Domain.Contracts.Repositories;
using scheduling_management.Domain.Entities;
using scheduling_management.Infra.Data;
using scheduling_management.Infra.Data;

namespace scheduling_management.Infra.Repositories;

public class ClientRepository(SchedulingManagementDbContext context) : Repository<Client>(context), IClientRepository
{
    public async Task<Client?> GetByIdWithAppointmentsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var a =  await context.Clients
            .AsNoTracking()
            .Include(c => c.Appointments)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        
        return a;
    }
}
