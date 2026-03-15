using Microsoft.EntityFrameworkCore;
using scheduling_management.Domain.Entities;
using scheduling_management.Infra.Data;
using scheduling_management.Infrastructure.Repositories;

namespace scheduling_management.Infra.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly SchedulingManagementDbContext _context;

    public ClientRepository(SchedulingManagementDbContext context)
    {
        _context = context;
    }

    public async Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Client?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Clients
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Client?> GetByIdWithAppointmentsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Clients
            .AsNoTracking()
            .Include(c => c.Appointments)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<PagedResult<Client>> GetPagedAsync(int page, int pageSize, Guid? establishmentId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Clients.AsNoTracking();
        if (establishmentId.HasValue)
            query = query.Where(c => c.EstablishmentId == establishmentId.Value);
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderBy(c => c.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        return new PagedResult<Client> { Items = items, TotalCount = totalCount };
    }

    public async Task<IReadOnlyList<Client>> SearchAsync(Guid? establishmentId, string? name, CancellationToken cancellationToken = default)
    {
        var query = _context.Clients.AsNoTracking();
        if (establishmentId.HasValue)
            query = query.Where(c => c.EstablishmentId == establishmentId.Value);
        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(c => c.Name.Contains(name));
        return await query.OrderBy(c => c.Name).ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Clients.AnyAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Client> CreateAsync(Client entity, CancellationToken cancellationToken = default)
    {
        await _context.Clients.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Client entity, CancellationToken cancellationToken = default)
    {
        _context.Clients.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Client entity, CancellationToken cancellationToken = default)
    {
        _context.Clients.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
