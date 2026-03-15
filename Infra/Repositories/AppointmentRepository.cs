using Microsoft.EntityFrameworkCore;
using scheduling_management.Domain.Entities;
using scheduling_management.Infra.Data;
using scheduling_management.Infrastructure.Repositories;

namespace scheduling_management.Infra.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly SchedulingManagementDbContext _context;

    public AppointmentRepository(SchedulingManagementDbContext context)
    {
        _context = context;
    }

    public async Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Appointments
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<Appointment?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Appointments
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<PagedResult<Appointment>> GetPagedAsync(int page, int pageSize, Guid? establishmentId = null, Guid? professionalId = null, Guid? clientId = null, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Appointments.AsNoTracking();
        if (establishmentId.HasValue)
            query = query.Where(a => a.EstablishmentId == establishmentId.Value);
        if (professionalId.HasValue)
            query = query.Where(a => a.ProfessionalId == professionalId.Value);
        if (clientId.HasValue)
            query = query.Where(a => a.ClientId == clientId.Value);
        if (fromDate.HasValue)
            query = query.Where(a => a.SchedulingDateOnly >= fromDate.Value);
        if (toDate.HasValue)
            query = query.Where(a => a.SchedulingDateOnly <= toDate.Value);
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderBy(a => a.SchedulingDateOnly).ThenBy(a => a.StartHours)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        return new PagedResult<Appointment> { Items = items, TotalCount = totalCount };
    }

    public async Task<IReadOnlyList<Appointment>> GetByProfessionalIdAsync(Guid professionalId, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Appointments.AsNoTracking().Where(a => a.ProfessionalId == professionalId);
        if (fromDate.HasValue)
            query = query.Where(a => a.SchedulingDateOnly >= fromDate.Value);
        if (toDate.HasValue)
            query = query.Where(a => a.SchedulingDateOnly <= toDate.Value);
        return await query.OrderBy(a => a.SchedulingDateOnly).ThenBy(a => a.StartHours).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Appointment>> GetByClientIdAsync(Guid clientId, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Appointments.AsNoTracking().Where(a => a.ClientId == clientId);
        if (fromDate.HasValue)
            query = query.Where(a => a.SchedulingDateOnly >= fromDate.Value);
        if (toDate.HasValue)
            query = query.Where(a => a.SchedulingDateOnly <= toDate.Value);
        return await query.OrderBy(a => a.SchedulingDateOnly).ThenBy(a => a.StartHours).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Appointment>> GetByEstablishmentIdAsync(Guid establishmentId, DateOnly? fromDate = null, DateOnly? toDate = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Appointments.AsNoTracking().Where(a => a.EstablishmentId == establishmentId);
        if (fromDate.HasValue)
            query = query.Where(a => a.SchedulingDateOnly >= fromDate.Value);
        if (toDate.HasValue)
            query = query.Where(a => a.SchedulingDateOnly <= toDate.Value);
        return await query.OrderBy(a => a.SchedulingDateOnly).ThenBy(a => a.StartHours).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Appointment>> SearchAsync(Guid? establishmentId, Guid? professionalId, Guid? clientId, DateOnly? fromDate, DateOnly? toDate, int? status, CancellationToken cancellationToken = default)
    {
        var query = _context.Appointments.AsNoTracking();
        if (establishmentId.HasValue)
            query = query.Where(a => a.EstablishmentId == establishmentId.Value);
        if (professionalId.HasValue)
            query = query.Where(a => a.ProfessionalId == professionalId.Value);
        if (clientId.HasValue)
            query = query.Where(a => a.ClientId == clientId.Value);
        if (fromDate.HasValue)
            query = query.Where(a => a.SchedulingDateOnly >= fromDate.Value);
        if (toDate.HasValue)
            query = query.Where(a => a.SchedulingDateOnly <= toDate.Value);
        if (status.HasValue)
            query = query.Where(a => (int)a.Status == status.Value);
        return await query.OrderBy(a => a.SchedulingDateOnly).ThenBy(a => a.StartHours).ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Appointments.AnyAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<Appointment> CreateAsync(Appointment entity, CancellationToken cancellationToken = default)
    {
        await _context.Appointments.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Appointment entity, CancellationToken cancellationToken = default)
    {
        _context.Appointments.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Appointment entity, CancellationToken cancellationToken = default)
    {
        _context.Appointments.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
