using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Domain.Entities;

namespace scheduling_management.Domain.Contracts.Repositories;

public interface IEstablishmentRepository : IRepository<Establishment>
{
    Task<List<Establishment>> GetAllEstablishment(CancellationToken ct = default);
};
