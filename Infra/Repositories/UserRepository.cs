using scheduling_management.Domain.Repository.Abstractions;
using scheduling_management.Domain.Entities;
using scheduling_management.Infra.Data;

namespace scheduling_management.Infra.Repositories;

public class UserRepository(SchedulingManagementDbContext context) 
    : Repository<User>(context), IUserRepository;
