using scheduling_management.Domain.Contracts.Repositories;
using scheduling_management.Domain.Entities;
using scheduling_management.Infra.Data;
using scheduling_management.Infra.Data;

namespace scheduling_management.Infra.Repositories;

public class ServiceRepository(SchedulingManagementDbContext context)
    : Repository<Service>(context), IServiceRepository;
