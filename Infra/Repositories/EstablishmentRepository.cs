using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Domain.Contracts.Repositories;
using scheduling_management.Domain.Entities;
using scheduling_management.Infra.Data;
using scheduling_management.Infra.Data;

namespace scheduling_management.Infra.Repositories;

public class EstablishmentRepository(SchedulingManagementDbContext context)
    : Repository<Establishment>(context), IEstablishmentRepository
{
    public Task<List<Establishment>> GetAllEstablishment(CancellationToken ct = default)
    {
        // todo: implementar busca de todas as empresas
        throw new NotImplementedException();
    }
}
