using System.Threading.Tasks;
using scheduling_management.Domain.UnitOfWork.Abstractions;

namespace scheduling_management.Infra.Data;

public class UnitOfWork(SchedulingManagementDbContext context) : IUnitOfWork
{
    public Task CommitAsync() =>  context.SaveChangesAsync();
}