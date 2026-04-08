using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using scheduling_management.Domain.Contracts.Repositories;
using scheduling_management.Domain.Entities;
using scheduling_management.Infra.Data;
using scheduling_management.Infra.Data;

namespace scheduling_management.Infra.Repositories;

public class AppointmentRepository(SchedulingManagementDbContext context)
    : Repository<Appointment>(context), IAppointmentRepository
{
    public async Task<List<Appointment>> GetByProfessionalIdAsync(Guid professionalId, CancellationToken cancellationToken)
    {
        var a = await context
            .Appointments
            .AsNoTracking()
            .Where(a => a.ProfessionalId == professionalId)
            .ToListAsync(cancellationToken);

        return a;
    }

    public async Task<List<Appointment>> GetByClientIdAsync(Guid clientId, CancellationToken cancellationToken)
    {
        var a = await context
            .Appointments
            .AsNoTracking()
            .Where(a => a.ClientId == clientId)
            .ToListAsync(cancellationToken);

        return a;
    }

    public async Task<List<Appointment>> GetByEstablishmentIdAsync(Guid establishmentId,
        CancellationToken cancellationToken = default)
    {
        var a = await context
            .Appointments
            .AsNoTracking()
            .Where(a => a.EstablishmentId == establishmentId)
            .ToListAsync(cancellationToken);

        return a;
    }
}
