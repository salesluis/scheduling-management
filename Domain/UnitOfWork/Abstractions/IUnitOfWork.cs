using System.Threading.Tasks;

namespace scheduling_management.Domain.UnitOfWork.Abstractions;

public interface IUnitOfWork
{
    Task CommitAsync();
}
