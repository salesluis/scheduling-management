using Microsoft.EntityFrameworkCore;
using scheduling_management.Domain.Entities;
using scheduling_management.Infra.Data;
using scheduling_management.Infrastructure.Repositories;

namespace scheduling_management.Infra.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SchedulingManagementDbContext _context;

    public UserRepository(SchedulingManagementDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<PagedResult<User>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _context.Users.AsNoTracking();
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderBy(u => u.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        return new PagedResult<User> { Items = items, TotalCount = totalCount };
    }

    public async Task<IReadOnlyList<User>> SearchAsync(string? name, string? email, CancellationToken cancellationToken = default)
    {
        var query = _context.Users.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(u => u.Name.Contains(name));
        if (!string.IsNullOrWhiteSpace(email))
            query = query.Where(u => u.Email.Contains(email));
        return await query.OrderBy(u => u.Name).ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.AnyAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User> CreateAsync(User entity, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(User entity, CancellationToken cancellationToken = default)
    {
        _context.Users.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(User entity, CancellationToken cancellationToken = default)
    {
        _context.Users.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
