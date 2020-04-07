using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Smilebox.Data.Contracts.Abstractions
{
    public interface IUnitOfWork
    {
        IQueryable<T> Get<T>() where T : class, IBaseEntity;
        T Create<T>(T item) where T : class, IBaseEntity;

        T Delete<T>(T item) where T : class, IBaseEntity;

        Task<int> CommitAsync(CancellationToken cancellationToken);
    }
}