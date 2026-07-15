using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Domain.Entities;

namespace scheduling_management.Domain.Repository.Abstractions;

public interface IProfessionalServiceRepository :  IRepository<ProfessionalService>
{
    Task<List<ProfessionalService>> GetByProfessionalIdAsync(Guid professionalId, CancellationToken cancellationToken);
    Task<List<ProfessionalService>> GetByServiceIdAsync(Guid serviceId, CancellationToken cancellationToken);
}
