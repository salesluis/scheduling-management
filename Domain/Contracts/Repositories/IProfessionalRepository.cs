using System;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Domain.Entities;

namespace scheduling_management.Domain.Contracts.Repositories;

public interface IProfessionalRepository :  IRepository<Professional>
{
    Task<Professional?> GetByIdWithAppointmentsAsync(Guid id, CancellationToken cancellationToken);
    Task<Professional?> GetByIdWithProfessionalServicesAsync(Guid id, CancellationToken cancellationToken);
}
