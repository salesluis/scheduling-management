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
        // todo: implementar a busca pelo id do estabelecimento
        // trazendo um predicate?
        var entities = await context
            .Set<T>()
            .AsNoTracking()
            .ToListAsync(ct);
        
        return entities;
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