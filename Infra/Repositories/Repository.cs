using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using scheduling_management.Domain.Contracts;
using scheduling_management.Domain.Contracts.Repositories;

namespace scheduling_management.Infra.Repositories;

public abstract class Repository<T>(DbContext context) :
    IRepository<T> where T : BaseEntity
{
    public async Task<List<T>> GetAllAsync(Guid establishmentId, CancellationToken ct)
    {
        IQueryable<T> query = context
            .Set<T>()
            .AsNoTracking();

        // If this entity is tenant-scoped, enforce tenant filter when provided.
        if (establishmentId != Guid.Empty && typeof(TenantEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(e =>
                EF.Property<Guid>(e, nameof(TenantEntity.EstablishmentId)) == establishmentId);
        }

        return await query.ToListAsync(ct);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var e = await context
            .Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, ct);
        return e;
    }

    public async Task<T> CreateAsync(T entity, CancellationToken ct)
    {
        await context.Set<T>().AddAsync(entity);
        return entity;
    }

    public void UpdateAsync(T entity, CancellationToken ct)
        => context.Set<T>().Update(entity);

    public void DeleteAsync(T entity, CancellationToken ct)
        =>context.Remove(entity);
}