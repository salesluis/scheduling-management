using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using scheduling_management.Domain.Contracts.Repositories;
using scheduling_management.Domain.Entities;
using scheduling_management.Infra.Data;
using scheduling_management.Infra.Data;

namespace scheduling_management.Infra.Repositories;

public class ProfessionalRepository(SchedulingManagementDbContext context)
    : Repository<Professional>(context), IProfessionalRepository
{
    public async Task<Professional?> GetByIdWithAppointmentsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var a =  await context.Professionals
            .AsNoTracking()
            .Include(p => p.Appointments)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        
        return a;
    }

    public async Task<Professional?> GetByIdWithProfessionalServicesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var a = await context.Professionals
            .AsNoTracking()
            .Include(p => p.ProfessionalServices)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        
        return a;
    }
}
