using System;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Domain.Entities;

namespace scheduling_management.Domain.Repository.Abstractions;


public interface IClientRepository : IRepository<Client>
{
    Task<Client?> GetByIdWithAppointmentsAsync(Guid id, CancellationToken cancellationToken = default);
}
