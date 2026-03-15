using Microsoft.EntityFrameworkCore;
using scheduling_management.Domain.Entities;
using scheduling_management.Infra.Data;
using scheduling_management.Infrastructure.Repositories;

namespace scheduling_management.Infra.Repositories;

public class ProfessionalRepository : IProfessionalRepository
{
    private readonly SchedulingManagementDbContext _context;

    public ProfessionalRepository(SchedulingManagementDbContext context)
    {
        _context = context;
    }

    public async Task<Professional?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Professionals
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Professional?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Professionals
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Professional?> GetByIdWithAppointmentsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Professionals
            .AsNoTracking()
            .Include(p => p.Appointments)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Professional?> GetByIdWithProfessionalServicesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Professionals
            .AsNoTracking()
            .Include(p => p.ProfessionalServices)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<PagedResult<Professional>> GetPagedAsync(int page, int pageSize, Guid? establishmentId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Professionals.AsNoTracking();
        if (establishmentId.HasValue)
            query = query.Where(p => p.EstablishmentId == establishmentId.Value);
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderBy(p => p.DisplayName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        return new PagedResult<Professional> { Items = items, TotalCount = totalCount };
    }

    public async Task<IReadOnlyList<Professional>> SearchAsync(Guid? establishmentId, string? displayName, bool? isActive, CancellationToken cancellationToken = default)
    {
        var query = _context.Professionals.AsNoTracking();
        if (establishmentId.HasValue)
            query = query.Where(p => p.EstablishmentId == establishmentId.Value);
        if (!string.IsNullOrWhiteSpace(displayName))
            query = query.Where(p => p.DisplayName.Contains(displayName));
        if (isActive.HasValue)
            query = query.Where(p => p.IsActive == isActive.Value);
        return await query.OrderBy(p => p.DisplayName).ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Professionals.AnyAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Professional> CreateAsync(Professional entity, CancellationToken cancellationToken = default)
    {
        await _context.Professionals.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Professional entity, CancellationToken cancellationToken = default)
    {
        _context.Professionals.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Professional entity, CancellationToken cancellationToken = default)
    {
        _context.Professionals.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
