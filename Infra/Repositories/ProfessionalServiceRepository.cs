using Microsoft.EntityFrameworkCore;
using scheduling_management.Domain.Entities;
using scheduling_management.Infra.Data;
using scheduling_management.Infrastructure.Repositories;

namespace scheduling_management.Infra.Repositories;

public class ProfessionalServiceRepository : IProfessionalServiceRepository
{
    private readonly SchedulingManagementDbContext _context;

    public ProfessionalServiceRepository(SchedulingManagementDbContext context)
    {
        _context = context;
    }

    public async Task<ProfessionalService?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.ProfessionalServices
            .AsNoTracking()
            .FirstOrDefaultAsync(ps => ps.Id == id, cancellationToken);
    }

    public async Task<ProfessionalService?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.ProfessionalServices
            .FirstOrDefaultAsync(ps => ps.Id == id, cancellationToken);
    }

    public async Task<PagedResult<ProfessionalService>> GetPagedAsync(int page, int pageSize, Guid? establishmentId = null, Guid? professionalId = null, Guid? serviceId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.ProfessionalServices.AsNoTracking();
        if (establishmentId.HasValue)
            query = query.Where(ps => ps.EstablishmentId == establishmentId.Value);
        if (professionalId.HasValue)
            query = query.Where(ps => ps.ProfessionalId == professionalId.Value);
        if (serviceId.HasValue)
            query = query.Where(ps => ps.ServiceId == serviceId.Value);
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderBy(ps => ps.ProfessionalId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        return new PagedResult<ProfessionalService> { Items = items, TotalCount = totalCount };
    }

    public async Task<IReadOnlyList<ProfessionalService>> GetByProfessionalIdAsync(Guid professionalId, CancellationToken cancellationToken = default)
    {
        return await _context.ProfessionalServices
            .AsNoTracking()
            .Where(ps => ps.ProfessionalId == professionalId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ProfessionalService>> GetByServiceIdAsync(Guid serviceId, CancellationToken cancellationToken = default)
    {
        return await _context.ProfessionalServices
            .AsNoTracking()
            .Where(ps => ps.ServiceId == serviceId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ProfessionalService>> SearchAsync(Guid? establishmentId, Guid? professionalId, Guid? serviceId, CancellationToken cancellationToken = default)
    {
        var query = _context.ProfessionalServices.AsNoTracking();
        if (establishmentId.HasValue)
            query = query.Where(ps => ps.EstablishmentId == establishmentId.Value);
        if (professionalId.HasValue)
            query = query.Where(ps => ps.ProfessionalId == professionalId.Value);
        if (serviceId.HasValue)
            query = query.Where(ps => ps.ServiceId == serviceId.Value);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.ProfessionalServices.AnyAsync(ps => ps.Id == id, cancellationToken);
    }

    public async Task<ProfessionalService> CreateAsync(ProfessionalService entity, CancellationToken cancellationToken = default)
    {
        await _context.ProfessionalServices.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(ProfessionalService entity, CancellationToken cancellationToken = default)
    {
        _context.ProfessionalServices.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ProfessionalService entity, CancellationToken cancellationToken = default)
    {
        _context.ProfessionalServices.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
