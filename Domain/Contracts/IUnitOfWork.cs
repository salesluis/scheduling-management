using System.Threading.Tasks;

namespace scheduling_management.Domain.Contracts;

public interface IUnitOfWork
{
    Task CommitAsync();
}