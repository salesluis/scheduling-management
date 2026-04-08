using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Domain.Contracts;

namespace scheduling_management.Domain.Contracts.Repositories;

public interface IRepository<T>
{
    Task<List<T>> GetAllAsync(Guid establishmentId,CancellationToken ct);
    Task<T?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<T> CreateAsync(T entity, CancellationToken ct);
    void UpdateAsync(T entity, CancellationToken ct);
    void DeleteAsync(T entity, CancellationToken ct);
}