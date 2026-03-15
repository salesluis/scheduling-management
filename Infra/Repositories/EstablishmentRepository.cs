using Microsoft.EntityFrameworkCore;
using scheduling_management.Domain.Entities;
using scheduling_management.Infra.Data;
using scheduling_management.Infrastructure.Repositories;

namespace scheduling_management.Infra.Repositories;

public class EstablishmentRepository : IEstablishmentRepository
{
    private readonly SchedulingManagementDbContext _context;

    public EstablishmentRepository(SchedulingManagementDbContext context)
    {
        _context = context;
    }

    public async Task<Establishment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Establishments
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<Establishment?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Establishments
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<PagedResult<Establishment>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _context.Establishments.AsNoTracking();
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderBy(e => e.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        return new PagedResult<Establishment> { Items = items, TotalCount = totalCount };
    }

    public async Task<IReadOnlyList<Establishment>> SearchAsync(string? name, bool? isActive, CancellationToken cancellationToken = default)
    {
        var query = _context.Establishments.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(e => e.Name.Contains(name));
        if (isActive.HasValue)
            query = query.Where(e => e.IsActive == isActive.Value);
        return await query.OrderBy(e => e.Name).ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Establishments.AnyAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<Establishment> CreateAsync(Establishment entity, CancellationToken cancellationToken = default)
    {
        await _context.Establishments.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Establishment entity, CancellationToken cancellationToken = default)
    {
        _context.Establishments.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Establishment entity, CancellationToken cancellationToken = default)
    {
        _context.Establishments.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
