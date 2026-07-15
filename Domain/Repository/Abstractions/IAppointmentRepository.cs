using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Domain.Entities;

namespace scheduling_management.Domain.Repository.Abstractions;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<List<Appointment>> GetByProfessionalIdAsync(Guid professionalId, CancellationToken cancellationToken);
    Task<List<Appointment>> GetByClientIdAsync(Guid clientId, CancellationToken cancellationToken );
    Task<List<Appointment>> GetByEstablishmentIdAsync(Guid establishmentId, CancellationToken cancellationToken );
}
