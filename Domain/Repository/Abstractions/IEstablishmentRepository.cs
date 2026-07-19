using scheduling_management.Domain.Entities;

namespace scheduling_management.Domain.Repository.Abstractions;

public interface IEstablishmentRepository : IRepository<Establishment>
{
    Task<List<Establishment>> GetAllEstablishment(CancellationToken ct = default);
};
