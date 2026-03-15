using Microsoft.EntityFrameworkCore;
using scheduling_management.Domain.Entities;
using scheduling_management.Infra.Data;
using scheduling_management.Infrastructure.Repositories;

namespace scheduling_management.Infra.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly SchedulingManagementDbContext _context;

    public ServiceRepository(SchedulingManagementDbContext context)
    {
        _context = context;
    }

    public async Task<Service?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Services
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<Service?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Services
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<PagedResult<Service>> GetPagedAsync(int page, int pageSize, Guid? establishmentId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Services.AsNoTracking();
        if (establishmentId.HasValue)
            query = query.Where(s => s.EstablishmentId == establishmentId.Value);
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderBy(s => s.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        return new PagedResult<Service> { Items = items, TotalCount = totalCount };
    }

    public async Task<IReadOnlyList<Service>> SearchAsync(Guid? establishmentId, string? name, bool? isActive, CancellationToken cancellationToken = default)
    {
        var query = _context.Services.AsNoTracking();
        if (establishmentId.HasValue)
            query = query.Where(s => s.EstablishmentId == establishmentId.Value);
        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(s => s.Name.Contains(name));
        if (isActive.HasValue)
            query = query.Where(s => s.IsActive == isActive.Value);
        return await query.OrderBy(s => s.Name).ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Services.AnyAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<Service> CreateAsync(Service entity, CancellationToken cancellationToken = default)
    {
        await _context.Services.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Service entity, CancellationToken cancellationToken = default)
    {
        _context.Services.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Service entity, CancellationToken cancellationToken = default)
    {
        _context.Services.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
